using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement;
using Orchard.Localization;
using Orchard.Services;
using Orchard.Security;
using Orchard.Roles.Services;
using Orchard.Data.Migration;

namespace Castle.NewsManagement
{
    public class Migrations : DataMigrationImpl
    {
        private readonly IContentManager _contentManager;

        private readonly IClock _clock;
        private readonly IMembershipService _membershipService;
        private readonly IRoleService _roleService;

        public Migrations(IContentManager contentManager,

            IClock clock,
            IMembershipService membershipService,
            IRoleService roleService)
        {
            _contentManager = contentManager;
            T = NullLocalizer.Instance;

            _clock = clock;
            _membershipService = membershipService;
            _roleService = roleService;
        }
        public Localizer T { get; set; }

        public int Create() 
        {
            SchemaBuilder.CreateTable("NewsItemPartRecord", table => table
                .ContentPartRecord()
                .Column<int>("NewsGroupPartRecord_Id")
            );
            SchemaBuilder.CreateTable("NewsGroupPartRecord", table => table
               .ContentPartRecord()
               .Column<string>("GroupName")
               .Column<string>("GroupDescription")
           );

            //定义新闻来源，但是现在不用
        /*
            SchemaBuilder.CreateTable("NewsSourcePartRecord", table => table
              .ContentPartRecord()
              .Column<string>("NewsSourcer")
              .Column<string>("NewsPublisher")
          );
          */
            ContentDefinitionManager.AlterTypeDefinition("NewsItem",cfg => cfg
               
               .WithPart("TitlePart") 
               .WithPart("BodyPart")
               .WithPart("NewsItemPart")
            //   .WithPart("NewsSourcePart")
              
               .WithPart("CommonPart")
           /*
           .WithPart("AutoroutePart", builder => builder
                .WithSetting("AutorouteSettings.AllowCustomPattern", "True")
                .WithSetting("AutorouteSettings.AutomaticAdjustmentOnEdit", "False")
                .WithSetting("AutorouteSettings.PatternDefinitions", "[{\"Name\":\"Title\",\"Pattern\":\"{Content.Slug}\",\"Description\":\"my-news\"}]")
                .WithSetting("AutorouteSettings.DefaultPatternIndex", "0"))

           .WithPart("IdentityPart") */
           );
          
           ContentDefinitionManager.AlterTypeDefinition("NewsGroup", builder => builder
               .DisplayedAs("新闻组")
               .WithPart("NewsGroupPart")
               .WithPart("IdentityPart")
               .WithPart("CommonPart",
                   p => p
                       .WithSetting("OwnerEditorSettings.ShowOwnerEditor", "false")
                       .WithSetting("DateEditorSettings.ShowDateEditor", "false"))
               
           );

           return 1;

        }
        public int UpdateFrom1()
        {
            _roleService.CreateRole("网站编辑");
            _roleService.CreatePermissionForRole("网站编辑", "AccessAdminPanel");
            _roleService.CreatePermissionForRole("网站编辑", "NewsAdmin");
            //编辑网站，可以管理标准的添加。
           // _roleService.CreatePermissionForRole("网站编辑", "StandardAdmin");
            _roleService.CreatePermissionForRole("网站编辑", "ManageOwnMedia");


            return 2;
        }

        
    }
}