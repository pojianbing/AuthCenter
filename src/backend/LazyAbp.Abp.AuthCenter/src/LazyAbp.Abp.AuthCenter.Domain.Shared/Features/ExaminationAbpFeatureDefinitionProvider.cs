using LazyAbp.Abp.AuthCenter.Localization;
using Volo.Abp.Features;
using Volo.Abp.Localization;
using Volo.Abp.Validation.StringValues;

namespace Xhznl.HelloAbp.Features
{
    public class ExaminationAbpFeatureDefinitionProvider : FeatureDefinitionProvider
    {
        public override void Define(IFeatureDefinitionContext context)
        {
            var group = context.AddGroup(ExaminationAbpFeatures.GroupName);

            group.AddFeature(ExaminationAbpFeatures.SocialLogins, "true", L("Feature:SocialLogins")
                , valueType: new ToggleStringValueType());
            group.AddFeature(ExaminationAbpFeatures.UserCount, "10", L("Feature:UserCount")
                , valueType: new FreeTextStringValueType(new NumericValueValidator(1, 1000)));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<AuthCenterResource>(name);
        }
    }
}