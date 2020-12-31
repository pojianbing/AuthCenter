<template>
  <div id="tags-view-container" class="tabs-bar-container">
    <el-tabs
      v-model="activeName"
      type="card"
      class="tabs-content"
      :class="['tabs-content-' + tagsStyle]"
      @tab-remove="closeSelectedTagByPath"
      @tab-click="handleSwitchRouter"
    >
      <el-tab-pane
        v-for="tag in visitedViews"
        :key="tag.path"
        ref="tag"
        :name="tag.path"
        :closable="!tag.meta || !tag.meta.affix"
      >
        <span
          slot="label"
        ><svg-icon v-if="tag.meta && tag.meta.icon" :icon-class="tag.meta.icon" /> {{ generateTitle(tag.title) }}</span>
      </el-tab-pane>
    </el-tabs>
    <el-dropdown @command="handleCommand">
      <span class="el-dropdown-link">更多<i class="el-icon-arrow-down el-icon--right" /> </span>
      <el-dropdown-menu slot="dropdown">
        <el-dropdown-item command="closeOther"><i class="el-icon-close" />关闭其他</el-dropdown-item>
        <el-dropdown-item command="closeLeft"><i class="el-icon-back" />关闭左侧</el-dropdown-item>
        <el-dropdown-item command="closeRight"><i class="el-icon-right" />关闭右侧</el-dropdown-item>
        <el-dropdown-item command="closeAll"><i class="el-icon-close" />关闭全部</el-dropdown-item>
      </el-dropdown-menu>
    </el-dropdown>
  </div>
</template>

<script>
import path from 'path'
import { generateTitle } from '@/utils/i18n'
import { mapState } from 'vuex'

export default {
  data() {
    return {
      top: 0,
      left: 0,
      affixTags: [],
      activeName: ''
    }
  },
  computed: {
    ...mapState({
      tagsStyle: state => state.settings.tagsStyle
    }),
    visitedViews() {
      var views = []

      this.$store.state.tagsView.visitedViews.forEach(view => {
        if (!view.meta) {
          view.meta = {}
        }
        if (!view.meta.icon) {
          view.meta.icon = this.findIcon(view.path)
        }
        views.push(view)
      })

      return views
    },
    routes() {
      return this.$store.state.permission.routes
    },
    selectedTag() {
      return this.visitedViews.find(e => e.path === this.activeName)
    },
    allViews() {
      const routes = []
      this.$store.state.permission.routes.forEach(r => {
        this.visitRoute(r, routes)
      })

      return routes
    }
  },
  watch: {
    $route() {
      this.addTags()
      this.activeName = this.$route.path
    }
  },
  mounted() {
    this.initTags()
    this.addTags()
    this.activeName = this.$route.path
  },
  methods: {
    generateTitle,
    isActive(route) {
      return route.path === this.$route.path
    },
    isAffix(tag) {
      return tag.meta && tag.meta.affix
    },
    visitRoute(parent, plainRoutes, basePath = '/') {
      const tagPath = path.resolve(basePath, parent.path)
      plainRoutes.push({
        fullPath: tagPath,
        path: tagPath,
        name: parent.name,
        meta: { ...parent.meta },
        parent: parent.parent
      })

      if (!parent.children) return

      parent.children.forEach(c => {
        c.parent = parent
        this.visitRoute(c, plainRoutes, tagPath)
      })
    },
    findIcon(path) {
      var route = this.allViews.find(e => e.path === path)
      const noIcon = !route.meta || !route.meta.icon
      const noParent = !route.parent

      if (!noIcon) return route.meta.icon

      // 包括父级在内没iocn
      if (noParent) {
        return 'link'
      } else {
        return this.findIcon(route.parent.path)
      }
    },
    handleCommand(command) {
      switch (command) {
        case 'closeAll':
          this.closeAllTags({})
          break
        case 'closeOther':
          this.closeOthersTags()
          break
        case 'closeLeft':
          this.closeLeftTags()
          break
        case 'closeRight':
          this.closeRightTags()
          break
      }
    },
    handleSwitchRouter() {
      this.$router.push({
        path: this.selectedTag.path,
        query: this.selectedTag.query,
        fullPath: this.selectedTag.fullPath
      })
    },
    filterAffixTags(routes, basePath = '/') {
      let tags = []
      routes.forEach(route => {
        if (route.meta && route.meta.affix) {
          const tagPath = path.resolve(basePath, route.path)
          tags.push({
            fullPath: tagPath,
            path: tagPath,
            name: route.name,
            meta: { ...route.meta }
          })
        }
        if (route.children) {
          const tempTags = this.filterAffixTags(route.children, route.path)
          if (tempTags.length >= 1) {
            tags = [...tags, ...tempTags]
          }
        }
      })
      return tags
    },
    initTags() {
      const affixTags = this.affixTags = this.filterAffixTags(this.routes)
      for (const tag of affixTags) {
        // Must have tag name
        if (tag.name) {
          this.$store.dispatch('tagsView/addVisitedView', tag)
        }
      }
    },
    addTags() {
      const { name } = this.$route
      if (name) {
        this.$store.dispatch('tagsView/addView', this.$route)
      }
      return false
    },
    // moveToCurrentTag () {
    //   const tags = this.$refs.tag
    //   console.log(tags.to)

    //   this.$nextTick(() => {
    //     for (const tag of tags) {
    //       if (tag.to.path === this.$route.path) {
    //         this.$refs.scrollPane.moveToTarget(tag)
    //         // when query is different then update
    //         if (tag.to.fullPath !== this.$route.fullPath) {
    //           this.$store.dispatch('tagsView/updateVisitedView', this.$route)
    //         }
    //         break
    //       }
    //     }
    //   })
    // },
    refreshSelectedTag(view) {
      this.$store.dispatch('tagsView/delCachedView', view).then(() => {
        const { fullPath } = view
        this.$nextTick(() => {
          this.$router.replace({
            path: '/redirect' + fullPath
          })
        })
      })
    },
    closeSelectedTagByPath(path) {
      var view = this.visitedViews.find(e => e.path === path)
      if (!view) return

      this.closeSelectedTag(view)
    },
    closeSelectedTag(view) {
      this.$store.dispatch('tagsView/delView', view).then(({ visitedViews }) => {
        if (this.isActive(view)) {
          this.toLastView(visitedViews, view)
        }
      })
    },
    closeOthersTags() {
      this.$router.push(this.selectedTag)
      this.$store.dispatch('tagsView/delOthersViews', this.selectedTag).then(() => {
        // this.moveToCurrentTag()
      })
    },
    closeAllTags(view) {
      this.$store.dispatch('tagsView/delAllViews').then(({ visitedViews }) => {
        if (this.affixTags.some(tag => tag.path === view.path)) {
          return
        }
        this.toLastView(visitedViews, view)
      })
    },
    closeLeftTags() {
      this.$router.push(this.selectedTag)
      this.$store.dispatch('tagsView/delLeftViews', this.selectedTag).then(() => {
        // this.moveToCurrentTag()
      })
    },
    closeRightTags() {
      this.$router.push(this.selectedTag)
      this.$store.dispatch('tagsView/delRightViews', this.selectedTag).then(() => {
        // this.moveToCurrentTag()
      })
    },
    toLastView(visitedViews, view) {
      const latestView = visitedViews.slice(-1)[0]
      if (latestView) {
        this.$router.push(latestView.fullPath)
      } else {
        // now the default is to redirect to the home page if there is no tags-view,
        // you can adjust it according to your needs.
        if (view.name === 'Dashboard') {
          // to reload home page
          this.$router.replace({ path: '/redirect' + view.fullPath })
        } else {
          this.$router.push('/')
        }
      }
    }
  }
}

</script>

<style lang="scss" >

.tabs-bar-container {
  position: relative;
  box-sizing: border-box;
  display: flex;
  align-content: center;
  align-items: center;
  justify-content: space-between;
  height: 50px;
  padding-right: 20px;
  padding-left: 20px;
  -webkit-user-select: none;
  -moz-user-select: none;
  -ms-user-select: none;
  user-select: none;
  background: #fff;
  border-top: 1px solid #f6f6f6;
}

/*** card start ***/

.tabs-bar-container .tabs-content-card {
  height: 34px;
}

.tabs-bar-container .tabs-content {
  width: calc(100% - 60px);
}
.tabs-bar-container .tabs-content-card .el-tabs__header,
.tabs-bar-container .tabs-content-smart .el-tabs__header,
.tabs-bar-container .tabs-content-smooth .el-tabs__header {
  border-bottom: 0;
}

.tabs-bar-container .tabs-content-card .el-tabs__header .el-tabs__item {
  box-sizing: border-box;
  height: 34px;
  margin-right: 5px;
  line-height: 34px;
  border: 1px solid #dcdfe6;
  border-radius: 2.5px;
  transition: padding 0.3s cubic-bezier(0.645, 0.045, 0.355, 1) !important;
}

.tabs-bar-container .tabs-content-card .el-tabs__header .el-tabs__item.is-active {
  color: #1890ff;
  background: rgba(24, 144, 255, 0.1);
  border: 1px solid #1890ff;
}

.tabs-bar-container .tabs-content-card .el-tabs__header .el-tabs__nav,
.tabs-bar-container .tabs-content-smart .el-tabs__header .el-tabs__nav,
.tabs-bar-container .tabs-content-smooth .el-tabs__header .el-tabs__nav {
  border: 0;
}

.tabs-bar-container .tabs-content-card .el-tabs__nav-next,
.tabs-bar-container .tabs-content-card .el-tabs__nav-prev,
.tabs-bar-container .tabs-content-smart .el-tabs__nav-next,
.tabs-bar-container .tabs-content-smart .el-tabs__nav-prev,
.tabs-bar-container .tabs-content-smooth .el-tabs__nav-next,
.tabs-bar-container .tabs-content-smooth .el-tabs__nav-prev {
  height: 34px;
  line-height: 34px;
}

.tabs-bar-container .tabs-content-card .el-tabs__item {
  font-size: 12px;
}

/*** card end ***/

/*** smart start ***/

.tabs-bar-container .tabs-content-smart  {
  height: 34px;
}

.tabs-bar-container .tabs-content-smart .el-tabs__header .el-tabs__item {
    height: 34px;
    margin-right: 5px;
    line-height: 34px;
    border: 0;
    transition: padding .3s cubic-bezier(.645,.045,.355,1)!important;
}

.tabs-bar-container .tabs-content-smart .el-tabs__header .el-tabs__item.is-active:after {
    width: 100%;
    transition: all .3s cubic-bezier(.645,.045,.355,1),border 0s,background 0s,color 0s,font-size 0s;
}

.tabs-bar-container .tabs-content-smart .el-tabs__header .el-tabs__item:after {
    position: absolute;
    bottom: 0;
    left: 0;
    width: 0;
    height: 2px;
    content: "";
    background-color: #1890ff;
    transition: all .3s cubic-bezier(.645,.045,.355,1),border 0s,background 0s,color 0s,font-size 0s;
}

.tabs-bar-container .tabs-content-smart .el-tabs__header .el-tabs__item.is-active {
    background: rgba(24,144,255,.1);
}

/*** smart end ***/

/*** smart start ***/
.tabs-bar-container .tabs-content-smooth  {
  height: 38px;
}

.tabs-bar-container .tabs-content-smooth .el-tabs__header .el-tabs__item {
    height: 38px;
    padding: 0 30px 0 30px;
    margin-top: 6px;
    margin-right: -15px;
    line-height: 38px;
    border: 0;
    transition: padding .3s cubic-bezier(.645,.045,.355,1)!important;
}

.tabs-bar-container .tabs-content-smooth .el-tabs__header .el-tabs__item.is-active, .tabs-bar-container .tabs-content-smooth .el-tabs__header .el-tabs__item.is-active:hover, .tabs-bar-container .tabs-content-smooth .el-tabs__header .el-tabs__item:hover {
    padding: 0 30px 0 30px;
    -webkit-mask: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAANoAAAAkCAYAAADvhSSyAAAACXBIWXMAAAsTAAALEwEAmpwYAAABbUlEQVR4nO3d4U3CUBSG4beJA9QNcAJhAtnAOoGOwCaOoBvoCDgBOIHdQDbAH7cJMaHtxcJB6/skJ6Thptw/H+cSktOCn5kCc+AWKJtraaxqYA28A8/N9cktgE9ga1n/tBYcqDhw/QtQHfoh0gitgdkpbrzg/N8klvWbKruz5Xa0CbAi/R6TtDMjdbdOF5k3qzBk0j4VRwzazbC9SKN1nbMo9+j4QTo+SvpuA1z2LcoN2nbYXqRR681R7tFR0gAGTQpg0KQABk0KYNCkAAZNCmDQpAAGTQpg0KQABk0KYNCkAAZNCmDQpAAGTQpg0KQABk0KYNCkAAZNGq4kjTRolRM0x31L3Sb0TMLKCZqTiaVu9/QErW+oyAQHp0p9NqRBqnXbgq6glcATdjQpRw1ctb3ZFrQSeAQejr8fabRegbvcxXPScfHcDxCwrL9YK/Y0qILdgwSnpNHfHhWl4ZbAW/O6LEgplHRC/mEtBfgClkhxraFbr7gAAAAASUVORK5CYII=);
    mask: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAANoAAAAkCAYAAADvhSSyAAAACXBIWXMAAAsTAAALEwEAmpwYAAABbUlEQVR4nO3d4U3CUBSG4beJA9QNcAJhAtnAOoGOwCaOoBvoCDgBOIHdQDbAH7cJMaHtxcJB6/skJ6Thptw/H+cSktOCn5kCc+AWKJtraaxqYA28A8/N9cktgE9ga1n/tBYcqDhw/QtQHfoh0gitgdkpbrzg/N8klvWbKruz5Xa0CbAi/R6TtDMjdbdOF5k3qzBk0j4VRwzazbC9SKN1nbMo9+j4QTo+SvpuA1z2LcoN2nbYXqRR681R7tFR0gAGTQpg0KQABk0KYNCkAAZNCmDQpAAGTQpg0KQABk0KYNCkAAZNCmDQpAAGTQpg0KQABk0KYNCkAAZNGq4kjTRolRM0x31L3Sb0TMLKCZqTiaVu9/QErW+oyAQHp0p9NqRBqnXbgq6glcATdjQpRw1ctb3ZFrQSeAQejr8fabRegbvcxXPScfHcDxCwrL9YK/Y0qILdgwSnpNHfHhWl4ZbAW/O6LEgplHRC/mEtBfgClkhxraFbr7gAAAAASUVORK5CYII=);
    -webkit-mask-size: 100% 100%;
    mask-size: 100% 100%;
}

.tabs-bar-container .tabs-content-smooth .el-tabs__header .el-tabs__item.is-active, .tabs-bar-container .tabs-content-smooth .el-tabs__header .el-tabs__item.is-active:hover {
    color: #1890ff;
    background: rgba(24,144,255,.1);
}

.tabs-bar-container .tabs-content-smooth .el-tabs__header .el-tabs__item {
    height: 38px;
    padding: 0 30px 0 30px;
    margin-top: 6px;
    margin-right: -15px;
    line-height: 38px;
    border: 0;
    transition: padding .3s cubic-bezier(.645,.045,.355,1)!important;
}

/*** smart end ***/

</style>
