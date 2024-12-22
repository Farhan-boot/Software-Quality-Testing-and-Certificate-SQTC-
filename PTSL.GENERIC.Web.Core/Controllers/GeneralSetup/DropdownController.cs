using Microsoft.AspNetCore.Mvc;

using PTSL.GENERIC.Web.Core.Helper;
using PTSL.GENERIC.Web.Core.Helper.Enum;
using PTSL.GENERIC.Web.Core.Helper.Enum.Beneficiary;
using PTSL.GENERIC.Web.Helper;

namespace PTSL.GENERIC.Web.Controllers.GeneralSetup
{
    public class DropdownController : Controller
    {
        public DropdownController(HttpHelper httpHelper)
        {
            

        }

        
        public ActionResult GetGenderEnumList()
        {
            return Json(EnumHelper.GetEnumDropdowns<Gender>(), SerializerOption.Default);
        }
        
        public ActionResult GetMaritalStatusEnumList()
        {
            return Json(EnumHelper.GetEnumDropdowns<MaritalStatus>(), SerializerOption.Default);
        }
        public ActionResult GetEducationLevelEnumList()
        {
            return Json(EnumHelper.GetEnumDropdowns<EducationLevel>(), SerializerOption.Default);
        }
        public ActionResult GetLandClassificationEnumList()
        {
            return Json(EnumHelper.GetEnumDropdowns<LandClassification>(), SerializerOption.Default);
        }
        public ActionResult GetHouseTypeEnumList()
        {
            return Json(EnumHelper.GetEnumDropdowns<HouseType>(), SerializerOption.Default);
        }
        public ActionResult GetDrinkingWaterResourceEnumList()
        {
            return Json(EnumHelper.GetEnumDropdowns<DrinkingWaterResource>(), SerializerOption.Default);
        }
        public ActionResult GetEducationalInstituteAccessibilityEnumList()
        {
            return Json(EnumHelper.GetEnumDropdowns<EducationalInstituteAccessibility>(), SerializerOption.Default);
        }
        public ActionResult GetSanitationFacilitiesEnumList()
        {
            return Json(EnumHelper.GetEnumDropdowns<SanitationFacilities>(), SerializerOption.Default);
        }
        public ActionResult GetSkillLevelEnumList()
        {
            return Json(EnumHelper.GetEnumDropdowns<SkillLevel>(), SerializerOption.Default);
        }
        public ActionResult GetSatisfactionLevelEnumList()
        {
            return Json(EnumHelper.GetEnumDropdowns<SatisfactionLevel>(), SerializerOption.Default);
        }
        public ActionResult GetForestDependencyEnumList()
        {
            return Json(EnumHelper.GetEnumDropdowns<ForestDependency>(), SerializerOption.Default);
        }
        public ActionResult GetFoodConditionEnumList()
        {
            return Json(EnumHelper.GetEnumDropdowns<FoodCondition>(), SerializerOption.Default);
        }
        public ActionResult GetFamilyMemberTypeEnumList()
        {
            return Json(EnumHelper.GetEnumDropdowns<FamilyMemberType>(), SerializerOption.Default);
        }

        public ActionResult GetMonthsEnumList()
        {
            return Json(EnumHelper.GetEnumDropdowns<Months>(), SerializerOption.Default);
        }
        
    }
}
