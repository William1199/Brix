using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Color",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Color", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConstructionType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConstructionType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contractors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    CreateBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contractors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaterialStores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Experience = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Place = table.Column<int>(type: "int", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    CreateBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialStores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Other",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Other", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Size",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Size", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Types",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "ContractorPosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Benefit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Required = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StarDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Place = table.Column<int>(type: "int", nullable: false),
                    PostCategories = table.Column<int>(type: "int", nullable: false),
                    Salaries = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ViewCount = table.Column<int>(type: "int", nullable: false),
                    NumberPeople = table.Column<int>(type: "int", nullable: false),
                    PeopeRemained = table.Column<int>(type: "int", nullable: false),
                    isApplied = table.Column<bool>(type: "bit", nullable: true),
                    QuizRequired = table.Column<bool>(type: "bit", nullable: false),
                    VideoRequired = table.Column<bool>(type: "bit", nullable: false),
                    ConstructionType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Accommodation = table.Column<bool>(type: "bit", nullable: false),
                    Transport = table.Column<bool>(type: "bit", nullable: false),
                    ContractorID = table.Column<int>(type: "int", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    CreateBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractorPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractorPosts_Contractors_ContractorID",
                        column: x => x.ContractorID,
                        principalTable: "Contractors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bill",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ContractorId = table.Column<int>(type: "int", nullable: true),
                    StoreID = table.Column<int>(type: "int", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bill", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bill_Contractors_ContractorId",
                        column: x => x.ContractorId,
                        principalTable: "Contractors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Bill_MaterialStores_StoreID",
                        column: x => x.StoreID,
                        principalTable: "MaterialStores",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitInStock = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoldQuantities = table.Column<int>(type: "int", nullable: false),
                    MaterialStoreID = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_MaterialStores_MaterialStoreID",
                        column: x => x.MaterialStoreID,
                        principalTable: "MaterialStores",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FromSystem = table.Column<bool>(type: "bit", nullable: false),
                    TypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Skills_Types_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Types",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Workers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExperienceDetail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Certificate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Experience = table.Column<int>(type: "int", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Place = table.Column<int>(type: "int", nullable: true),
                    ConstructionType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    CreateBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Workers_Types_TypeID",
                        column: x => x.TypeID,
                        principalTable: "Types",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ContractorPostType",
                columns: table => new
                {
                    ContractorPostID = table.Column<int>(type: "int", nullable: false),
                    TypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractorPostType", x => new { x.TypeID, x.ContractorPostID });
                    table.ForeignKey(
                        name: "FK_ContractorPostType_ContractorPosts_ContractorPostID",
                        column: x => x.ContractorPostID,
                        principalTable: "ContractorPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContractorPostType_Types_TypeID",
                        column: x => x.TypeID,
                        principalTable: "Types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Quiz",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PostID = table.Column<int>(type: "int", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quiz", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Quiz_ContractorPosts_PostID",
                        column: x => x.PostID,
                        principalTable: "ContractorPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Quiz_Types_TypeID",
                        column: x => x.TypeID,
                        principalTable: "Types",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    CategoriesID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => new { x.ProductID, x.CategoriesID });
                    table.ForeignKey(
                        name: "FK_ProductCategories_Categories_CategoriesID",
                        column: x => x.CategoriesID,
                        principalTable: "Categories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductCategories_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ColorId = table.Column<int>(type: "int", nullable: true),
                    SizeID = table.Column<int>(type: "int", nullable: true),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtherID = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductTypes_Color_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Color",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductTypes_Other_OtherID",
                        column: x => x.OtherID,
                        principalTable: "Other",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductTypes_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductTypes_Size_SizeID",
                        column: x => x.SizeID,
                        principalTable: "Size",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ContractorPostSkills",
                columns: table => new
                {
                    ContractorPostID = table.Column<int>(type: "int", nullable: false),
                    SkillID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractorPostSkills", x => new { x.SkillID, x.ContractorPostID });
                    table.ForeignKey(
                        name: "FK_ContractorPostSkills_ContractorPosts_ContractorPostID",
                        column: x => x.ContractorPostID,
                        principalTable: "ContractorPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContractorPostSkills_Skills_SkillID",
                        column: x => x.SkillID,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkerID = table.Column<int>(type: "int", nullable: false),
                    PostID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Group_Workers_WorkerID",
                        column: x => x.WorkerID,
                        principalTable: "Workers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PostInvite",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractorId = table.Column<int>(type: "int", nullable: true),
                    WorkerID = table.Column<int>(type: "int", nullable: true),
                    ContractorPostId = table.Column<int>(type: "int", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostInvite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostInvite_ContractorPosts_ContractorPostId",
                        column: x => x.ContractorPostId,
                        principalTable: "ContractorPosts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PostInvite_Contractors_ContractorId",
                        column: x => x.ContractorId,
                        principalTable: "Contractors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PostInvite_Workers_WorkerID",
                        column: x => x.WorkerID,
                        principalTable: "Workers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    CreateBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContractorId = table.Column<int>(type: "int", nullable: true),
                    WorkerID = table.Column<int>(type: "int", nullable: true),
                    MaterialStoreID = table.Column<int>(type: "int", nullable: true),
                    Provider = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Contractors_ContractorId",
                        column: x => x.ContractorId,
                        principalTable: "Contractors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_MaterialStores_MaterialStoreID",
                        column: x => x.MaterialStoreID,
                        principalTable: "MaterialStores",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_Workers_WorkerID",
                        column: x => x.WorkerID,
                        principalTable: "Workers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WorkerContructionTypes",
                columns: table => new
                {
                    ConstructionTypeId = table.Column<int>(type: "int", nullable: false),
                    BuilderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkerContructionTypes", x => new { x.ConstructionTypeId, x.BuilderId });
                    table.ForeignKey(
                        name: "FK_WorkerContructionTypes_ConstructionType_ConstructionTypeId",
                        column: x => x.ConstructionTypeId,
                        principalTable: "ConstructionType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkerContructionTypes_Workers_BuilderId",
                        column: x => x.BuilderId,
                        principalTable: "Workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkerSkills",
                columns: table => new
                {
                    WorkerSkillID = table.Column<int>(type: "int", nullable: false),
                    SkillID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkerSkills", x => new { x.WorkerSkillID, x.SkillID });
                    table.ForeignKey(
                        name: "FK_WorkerSkills_Skills_SkillID",
                        column: x => x.SkillID,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkerSkills_Workers_WorkerSkillID",
                        column: x => x.WorkerSkillID,
                        principalTable: "Workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuizId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_Quiz_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quiz",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BillDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BillID = table.Column<int>(type: "int", nullable: true),
                    ProductID = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductTypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillDetail_Bill_BillID",
                        column: x => x.BillID,
                        principalTable: "Bill",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BillDetail_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BillDetail_ProductTypes_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "ProductTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppliedPost",
                columns: table => new
                {
                    PostID = table.Column<int>(type: "int", nullable: false),
                    WorkerID = table.Column<int>(type: "int", nullable: false),
                    GroupID = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    AppliedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WishSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    QuizId = table.Column<int>(type: "int", nullable: true),
                    Video = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppliedPost", x => new { x.PostID, x.WorkerID });
                    table.ForeignKey(
                        name: "FK_AppliedPost_ContractorPosts_PostID",
                        column: x => x.PostID,
                        principalTable: "ContractorPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppliedPost_Group_GroupID",
                        column: x => x.GroupID,
                        principalTable: "Group",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppliedPost_Quiz_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quiz",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppliedPost_Workers_WorkerID",
                        column: x => x.WorkerID,
                        principalTable: "Workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Commitment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostID = table.Column<int>(type: "int", nullable: true),
                    WorkerID = table.Column<int>(type: "int", nullable: false),
                    ContractorID = table.Column<int>(type: "int", nullable: false),
                    GroupID = table.Column<int>(type: "int", nullable: true),
                    OptionalTerm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salaries = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commitment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Commitment_ContractorPosts_PostID",
                        column: x => x.PostID,
                        principalTable: "ContractorPosts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Commitment_Contractors_ContractorID",
                        column: x => x.ContractorID,
                        principalTable: "Contractors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Commitment_Group_GroupID",
                        column: x => x.GroupID,
                        principalTable: "Group",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Commitment_Workers_WorkerID",
                        column: x => x.WorkerID,
                        principalTable: "Workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupMember",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    SkillAssessment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BehaviourAssessment = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupMember", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupMember_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GroupMember_Types_TypeID",
                        column: x => x.TypeID,
                        principalTable: "Types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    TypeID = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => new { x.UserID, x.ProductID, x.Id });
                    table.ForeignKey(
                        name: "FK_Carts_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Carts_ProductTypes_TypeID",
                        column: x => x.TypeID,
                        principalTable: "ProductTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Carts_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdentitficationCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FaceImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FrontID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BackID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessLicense = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdentificateType = table.Column<int>(type: "int", nullable: false),
                    PreCodition = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentitficationCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentitficationCards_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NavigateId = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpireationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransactionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsRefund = table.Column<bool>(type: "bit", nullable: true),
                    VnPayResponseCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExtendDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    CreateBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractorPostId = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    ReportProblem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reports_ContractorPosts_ContractorPostId",
                        column: x => x.ContractorPostId,
                        principalTable: "ContractorPosts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reports_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reports_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Saves",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractorPostId = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Saves", x => new { x.Id, x.UserId });
                    table.ForeignKey(
                        name: "FK_Saves_ContractorPosts_ContractorPostId",
                        column: x => x.ContractorPostId,
                        principalTable: "ContractorPosts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Saves_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isCorrect = table.Column<bool>(type: "bit", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAnswer",
                columns: table => new
                {
                    WorkerID = table.Column<int>(type: "int", nullable: false),
                    AnswerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAnswer", x => new { x.WorkerID, x.AnswerID });
                    table.ForeignKey(
                        name: "FK_UserAnswer_Answers_AnswerID",
                        column: x => x.AnswerID,
                        principalTable: "Answers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAnswer_Workers_WorkerID",
                        column: x => x.WorkerID,
                        principalTable: "Workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { 1, "Xuất xứ" },
                    { 2, "Chất liệu" },
                    { 3, "Phong cách " },
                    { 4, "Vị trí " }
                });

            migrationBuilder.InsertData(
                table: "Color",
                columns: new[] { "Id", "Image", "LastModifiedAt", "Name" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2023, 4, 22, 15, 17, 18, 794, DateTimeKind.Local).AddTicks(1846), "No Color" },
                    { 6, null, new DateTime(2023, 4, 22, 15, 17, 18, 794, DateTimeKind.Local).AddTicks(1083), "Màu vàng" },
                    { 7, null, new DateTime(2023, 4, 22, 15, 17, 18, 794, DateTimeKind.Local).AddTicks(1097), "Màu xám" }
                });

            migrationBuilder.InsertData(
                table: "ConstructionType",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Nhà ở" },
                    { 2, "Chung cư " },
                    { 3, "Công trình công cộng" }
                });

            migrationBuilder.InsertData(
                table: "Contractors",
                columns: new[] { "Id", "CompanyName", "CreateBy", "Description", "LastModifiedAt", "Website" },
                values: new object[,]
                {
                    { 1, "Công ty xây dưng Khang An", new Guid("d7285fb7-835b-4680-a18c-673bd71f63d7"), "Hoạt động chính trong lĩnh vực: tư vấn, thiết kế, trang trí nội ngoại thất, lập dự toán công trình và xây dựng nhà ở tư nhân, nhà phố, biệt thự – vila, quán Bar – sân vườn, khách sạn, nhà hàng, showroom… Sản phẩm của chúng tôi được xây dựng theo quy trình kiểm tra chất lượng nghiêm ngặt của hệ thống quản lý chất lượng ISO 9001:2008.", new DateTime(2023, 4, 22, 15, 17, 18, 755, DateTimeKind.Local).AddTicks(395), "nhaxanhqn.com" },
                    { 2, "Công ty xây dưng Đất Xanh", new Guid("d7285fb7-835b-4680-a18c-673bd71f63d6"), "Trong những năm vừa qua được sự ưu ái và tín nhiệm của Quý khách hàng Công ty Đất Xanh từng bước trưởng thành và trở thành đơn vị hoạt động trong lĩnh vực tư vấn, thiết kế và xây dựng dân dụng hàng đầu tại Việt Nam.", new DateTime(2023, 4, 22, 15, 17, 18, 764, DateTimeKind.Local).AddTicks(7238), "xaydunglaco.vn" },
                    { 3, "Công ty xây dưng Ánh Nhiên Xanh", new Guid("86b8070e-00c5-45de-8db7-199cee7350d9"), "Trong những năm vừa qua được sự ưu ái và tín nhiệm của Quý khách hàng Công ty Ánh Nhiên Xanh từng bước trưởng thành và trở thành đơn vị hoạt động trong lĩnh vực tư vấn, thiết kế và xây dựng dân dụng hàng đầu tại Việt Nam.", new DateTime(2023, 4, 22, 15, 17, 18, 774, DateTimeKind.Local).AddTicks(3025), "xaydunganhnhien.vn" }
                });

            migrationBuilder.InsertData(
                table: "MaterialStores",
                columns: new[] { "Id", "CreateBy", "Description", "Experience", "Image", "LastModifiedAt", "Place", "TaxCode", "Website" },
                values: new object[,]
                {
                    { 1, new Guid("b57b172a-a044-11ed-a8fc-0242ac120002"), "Với mục tiêu cung cấp nhiều gói sản phẩm phong phú về mẫu mã và các tính năng linh hoạt cho nhiều loại hình website như giới thiệu công ty, bán hàng, trang tin tức, thương mại điện tử… cùng với nhiều giao diện phong phú đa dạng độc đáo đã được VNS lọc chọn và đúc kết nhằm giới thiệu tới khách hàng với mong muốn có một website nhanh, đẹp, hiệu quả và giá cả hợp lý.", "Hiện đang là đại lý cấp 1 phân phối các sản phẩm chất lượng, có thương hiệu nổi tiếng, giá thành phù hợp với giá niêm yết của nhà máy. Công ty còn cung cấp và phân phối nhiều loại cát bê tông và cát xây dựng được sàng và rửa tại dây chuyền sản xuất. Ngoài ra còn phân phối nhiều loại vật liệu xây dựng khác như xi măng, gạch, sắt thép đảm bảo chất lượng cao để sử dụng cho các công trình xây dựng.", null, new DateTime(2023, 4, 22, 15, 17, 18, 784, DateTimeKind.Local).AddTicks(358), 61, "8156184163", "https://vinasoftware.com.vn/" },
                    { 2, new Guid("be21b564-a044-11ed-a8fc-0242ac120002"), "Về vật liệu xây dựng, công ty luôn có sẵn hàng hóa để khách hàng so sánh và lựa chọn, ngoài ra còn có đội xe lớn nhỏ sẵn sàng giao hàng trong thời gian sớm nhất.", "Hiện đang là đại lý cấp 1 phân phối các sản phẩm chất lượng, có thương hiệu nổi tiếng, giá thành phù hợp với giá niêm yết của nhà máy. Công ty còn cung cấp và phân phối nhiều loại cát bê tông và cát xây dựng được sàng và rửa tại dây chuyền sản xuất. Ngoài ra còn phân phối nhiều loại vật liệu xây dựng khác như xi măng, gạch, sắt thép đảm bảo chất lượng cao để sử dụng cho các công trình xây dựng.", null, new DateTime(2023, 4, 22, 15, 17, 18, 793, DateTimeKind.Local).AddTicks(9589), 61, "8156284563", null }
                });

            migrationBuilder.InsertData(
                table: "Other",
                columns: new[] { "Id", "Image", "LastModifiedAt", "Name" },
                values: new object[] { 1, null, new DateTime(2023, 4, 22, 15, 17, 18, 794, DateTimeKind.Local).AddTicks(1878), "No Other" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("20efd516-f16c-41b3-b11d-bc908cd2056b"), "0ae1eefe-22d5-4692-95b5-8b5892456d99", "Contractor", "Contractor", "CONTRACTOR" },
                    { new Guid("52ec6e78-6732-43bf-adab-9cfa2e5da268"), "85141db0-d9a9-4b6d-990d-998723810b88", "Admin", "Admin", "ADMIN" },
                    { new Guid("a4fbc29e-9749-4ea0-bcaa-67fc9f104bd1"), "e1c2541c-3257-41b6-b81c-ced3b2dc1936", "Store", "Store", "STORE" },
                    { new Guid("dc48ba58-ddcb-41de-96fe-e41327e5f313"), "f24ccfcf-2f13-48b1-9920-e99062693de4", "User", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "Size",
                columns: new[] { "Id", "LastModifiedAt", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 4, 22, 15, 17, 18, 794, DateTimeKind.Local).AddTicks(1857), "No Size" },
                    { 2, new DateTime(2023, 4, 22, 15, 17, 18, 794, DateTimeKind.Local).AddTicks(717), "4.5L" },
                    { 3, new DateTime(2023, 4, 22, 15, 17, 18, 794, DateTimeKind.Local).AddTicks(732), "7.5L" },
                    { 4, new DateTime(2023, 4, 22, 15, 17, 18, 794, DateTimeKind.Local).AddTicks(1108), "5M" },
                    { 5, new DateTime(2023, 4, 22, 15, 17, 18, 794, DateTimeKind.Local).AddTicks(1119), "10M" },
                    { 7, new DateTime(2023, 4, 22, 15, 17, 18, 794, DateTimeKind.Local).AddTicks(1670), "5L" },
                    { 8, new DateTime(2023, 4, 22, 15, 17, 18, 794, DateTimeKind.Local).AddTicks(1683), "18L" },
                    { 10, new DateTime(2023, 4, 22, 15, 17, 18, 794, DateTimeKind.Local).AddTicks(1546), "6 bóng" },
                    { 11, new DateTime(2023, 4, 22, 15, 17, 18, 794, DateTimeKind.Local).AddTicks(1555), "8 bóng" },
                    { 14, new DateTime(2023, 4, 22, 15, 17, 18, 794, DateTimeKind.Local).AddTicks(1485), "Giường 1,8m" },
                    { 15, new DateTime(2023, 4, 22, 15, 17, 18, 794, DateTimeKind.Local).AddTicks(1496), "Giường 2m" }
                });

            migrationBuilder.InsertData(
                table: "Types",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("4ace8fcb-95eb-48c0-9deb-240e8b4e10e0"), "Thợ xây" },
                    { new Guid("bd880489-5c76-4854-93ab-66e3a541bf24"), "Thợ hồ" },
                    { new Guid("ce9fa65b-d005-46b6-953e-e6462a59cfb3"), "Thợ sơn" },
                    { new Guid("cf9fa65b-d005-46b6-953e-e6462a59cfb3"), "Thợ hàn" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("dc48ba58-ddcb-41de-96fe-e41327e5f313"), new Guid("17c76dfe-7a0b-4ac9-ab8b-ba95e588a135") },
                    { new Guid("dc48ba58-ddcb-41de-96fe-e41327e5f313"), new Guid("319d2a06-92cc-434d-abce-7e8a33650a0d") },
                    { new Guid("20efd516-f16c-41b3-b11d-bc908cd2056b"), new Guid("86b8070e-00c5-45de-8db7-199cee7350d9") },
                    { new Guid("dc48ba58-ddcb-41de-96fe-e41327e5f313"), new Guid("8f314589-0c7c-40a4-b5bc-c73639664922") },
                    { new Guid("a4fbc29e-9749-4ea0-bcaa-67fc9f104bd1"), new Guid("b57b172a-a044-11ed-a8fc-0242ac120002") },
                    { new Guid("a4fbc29e-9749-4ea0-bcaa-67fc9f104bd1"), new Guid("be21b564-a044-11ed-a8fc-0242ac120002") },
                    { new Guid("dc48ba58-ddcb-41de-96fe-e41327e5f313"), new Guid("d39ae0a6-9b2d-4421-be4a-cc294cec054f") }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("20efd516-f16c-41b3-b11d-bc908cd2056b"), new Guid("d7285fb7-835b-4680-a18c-673bd71f63d6") },
                    { new Guid("20efd516-f16c-41b3-b11d-bc908cd2056b"), new Guid("d7285fb7-835b-4680-a18c-673bd71f63d7") },
                    { new Guid("dc48ba58-ddcb-41de-96fe-e41327e5f313"), new Guid("d7285fb7-835b-4680-a18c-673bd71f63d9") },
                    { new Guid("52ec6e78-6732-43bf-adab-9cfa2e5da268"), new Guid("d7285fb7-835b-4680-a18c-673bd71f63e9") },
                    { new Guid("dc48ba58-ddcb-41de-96fe-e41327e5f313"), new Guid("d91f9ece-25a7-4dc6-adde-186b12c04d56") }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "Address", "Avatar", "WorkerID", "ConcurrencyStamp", "ContractorId", "CreateBy", "DOB", "Email", "EmailConfirmed", "FirstName", "Gender", "IdNumber", "LastModifiedAt", "LastName", "LockoutEnabled", "LockoutEnd", "MaterialStoreID", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Provider", "RefreshTokenExpiryTime", "SecurityStamp", "Status", "Token", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("d7285fb7-835b-4680-a18c-673bd71f63e9"), 0, "18 Tô Ký , Huyện Châu Thành , Đà Nẵng", "https://i1-giaitri.vnecdn.net/2013/08/15/DK-02756-1376528749.jpg?w=680&h=0&q=100&dpr=1&fit=crop&s=mX89l0q4HQgntQ5wJesOcw", null, "ba3a6d63-b294-4c5b-b6d0-7673a69ad362", null, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2001, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin15@gmail.com", false, "Admin", 0, null, new DateTime(2023, 4, 22, 15, 17, 18, 665, DateTimeKind.Local).AddTicks(7317), "Admin", false, null, null, null, null, "AQAAAAEAACcQAAAAEOWkzRGz+BcpzOdMoxfkBhjzEo37CEUYLPAGKajkzRELqFW1aRBYoqNcNehJlwyQVg==", "0909090909", false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 3, "xxx", false, "admin15@gmail.com" });

            migrationBuilder.InsertData(
                table: "ContractorPosts",
                columns: new[] { "Id", "Accommodation", "Benefit", "ConstructionType", "ContractorID", "CreateBy", "Description", "EndDate", "EndTime", "LastModifiedAt", "NumberPeople", "PeopeRemained", "Place", "PostCategories", "ProjectName", "QuizRequired", "Required", "Salaries", "StarDate", "StartTime", "Status", "Title", "Transport", "VideoRequired", "ViewCount", "isApplied" },
                values: new object[,]
                {
                    { 1, true, "<div>- Tăng lương 6 tháng một lần. Thưởng vào các ngày lễ tết. Cơ hội thăng tiến cao.</div><div>- Xe đưa rước hàng ngày</div><div>- Khám sức khỏe &amp; đi du lịch và các chế độ khác theo quy định của pháp luật Lao động</div>", "Nhà ở", 1, new Guid("d7285fb7-835b-4680-a18c-673bd71f63d7"), "<div>I. THÔNG TIN CHUNG</div><div>1. Quy trình công việc liên quan: Quy trình quản lí dự án (mảng bản vẽ, báo giá, tiến độ)</div><div>2. Cấp trực tiếp quản lý: Giám đốc dự án</div><div>3. Loại hợp đồng: Hợp đồng xác định có thời hạn/không thời hạn</div><div><br></div><div>II. MỤC ĐÍCH CÔNG VIỆC</div><div>Nắm bản vẽ của dự án từ lúc đấu thầu, hiểu rõ các spect của dự án để báo giá. Có khả năng bốc khối lượng để đối ứng với cá báo giá gấp, các hạng mục phát sinh. Khi dự án trúng thấu, có khả năng điều phối dự án ở vai trò quản lí thiết kế, quản lí tiến độ, hoặc quản lí chất lượng (đối với dự án quy mô nhỏ)</div><div><br></div><div>III. TRÁCH NHIỆM VÀ NHIỆM VỤ</div><div>1. Làm báo giá dự án Nhật và hỗ trợ giám đốc dự án đi đấu thầu</div><div>• Nắm rõ bản vẽ của dự án từ lúc đấu thầu</div><div>• Hiểu rõ các vật tư spect của ngành kết cấu thép để báo giá</div><div>• Có khả năng bốc khối lượng cho các dự án gấp</div><div>• Có khả năng lên các bản vẽ đề xuất bằng CAD</div><div>• Cùng với GDDA đi đấu thầu các dự án</div><div>• Tiếp khách, đối ứng khách khi có audit, khách về việt nam</div><div><br></div><div>2. Quản lí dự án trúng thầu mảng quản lí bản vẽ thiết kế, quản lí tiến độ</div><div>• Nắm rõ các thay đổi thiết kế, chỉ thị bản vẽ của khách để triển khai cho shop</div><div>• Sử dụng thành thạo Tekla hoặc phần mềm real 4 để xuất các giấy tờ phục vụ cho quản lí dự án</div><div>• Lên được kế hoạch sản xuất và quản lí sản xuất, xuất hàng</div><div><br></div><div>3. Đối ứng khách hàng</div><div>• Báo cáo tiến độ sản xuất, bản vẽ</div><div>• Tham gia chủ đạo trong các cuộc họp tiến độ với nhà máy</div><div><br></div><div>4 . Nghiên cứu &amp; Phát triển</div><div>• Tham gia nghiên cứu và phát triển giải pháp công nghệ sản xuất</div><div><br></div><div>5. Tổng kết và đúc kết kinh nghiệm quản lí dự án</div><div>• Tổng kết thường xuyên các vướng mắt trong tiến độ, bản vẽ . Các lỗi hay mắc phải, hướng xử lí</div><div>• Đối với NCR đóng vai trò chỉ huy xử lí (liên quan tiến độ, chất lượng)</div><div><br></div><div>6. Cost control</div><div>• Phối hợp với giám đốc dự án trong công tác giám sát ngân sách thực hiện</div><div><br></div><div>7. Nhiệm vụ khác</div><div>• Các nhiệm vụ khác được phân công</div>", new DateTime(2023, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "17:30", new DateTime(2023, 4, 22, 15, 17, 18, 793, DateTimeKind.Local).AddTicks(9809), 20, 0, 52, 1, "TPHCM - QUẬN 7 - TUYỂN DỤNG GẤP", true, "<div>1. Trình độ đào tạo:</div><div>• Bằng cấp tối thiểu: Cao đẳng, Đại học</div><div>• Chuyên ngành đào tạo: Xây dựng Dân dụng và Công nghiệp, Cơ khí</div><div>• Chứng chỉ, giấy phép hành nghề (nếu có): không yêu cầu.Ưu tiên nếu có chứng chỉ:….</div><div>• Trình độ Ngoại ngữ (tiếng anh, tiếng nhật, tiếng trung): Tiếng Nhật (bắt buộc, N2 đổ lên )</div><div><br></div><div>2. Kiến thức cần có:</div><div>• Sản phẩm dịch vụ của Công ty</div><div>• Kiến thức về lĩnh vực kết cấu thép (tiêu chuẩn, vật liệu, sản xuất, quy trình thi công, lắp dựng)</div><div><br></div><div>3. Năng lực cần thiết:</div><div>Năng lực quản lý</div><div>•Nếu có kinh nghiệm làm ở Nhật, hoặc ở công ty Nhật lâu năm càng tốt</div><div>Năng lực chuyên môn</div><div>•Có bằng cấp liên quan đến kết cấu thép Nhật càng tốt</div><div>•Kỹ năng vẽ CAD cơ bản, Sử dụng thành thạo Tekla hoặc phần mềm real 4</div><div>•Đọc hiểu bản vẽ lắp dựng, bản vẽ tổng thể, tiêu chuẩn đường hàn, tiêu chuẩn JASS 6, ASTM , …</div><div><br></div><div>Năng lực bổ trợ</div><div>• Kỹ năng giao tiếp</div><div>• Kỹ năng giải quyết vấn đề và ra quyết định</div><div>• Kỹ năng làm việc nhóm</div><div>• Kỹ năng sử dụng thành thạo tin học văn phòng</div><div><br></div><div>4. Thái độ hoặc tố chất</div><div>• Có tinh thần trách nhiệm</div><div>• Cẩn thận, tỉ mỉ, chủ động, cầu tiến</div><div><br></div><div>5. Yêu cầu khác:</div><div>•Có đủ sức khỏe để đáp ứng yêu cầu nhiệm vụ được giao</div><div>•Có thể đi công tác</div>", "200000 - 800000", new DateTime(2023, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "8:00", 3, "Tuyển dụng thợ xây lành nghề CẦN GẤP", true, false, 0, null },
                    { 2, true, "<div>- Tăng lương 6 tháng một lần. Thưởng vào các ngày lễ tết. Cơ hội thăng tiến cao.</div><div>- Xe đưa rước hàng ngày</div><div>- Khám sức khỏe &amp; đi du lịch và các chế độ khác theo quy định của pháp luật Lao động</div>", "Tòa nhà/Chung cư", 1, new Guid("d7285fb7-835b-4680-a18c-673bd71f63d7"), "<div>I. THÔNG TIN CHUNG</div><div>1. Quy trình công việc liên quan: Quy trình quản lí dự án (mảng bản vẽ, báo giá, tiến độ)</div><div>2. Cấp trực tiếp quản lý: Giám đốc dự án</div><div>3. Loại hợp đồng: Hợp đồng xác định có thời hạn/không thời hạn</div><div><br></div><div>II. MỤC ĐÍCH CÔNG VIỆC</div><div>Nắm bản vẽ của dự án từ lúc đấu thầu, hiểu rõ các spect của dự án để báo giá. Có khả năng bốc khối lượng để đối ứng với cá báo giá gấp, các hạng mục phát sinh. Khi dự án trúng thấu, có khả năng điều phối dự án ở vai trò quản lí thiết kế, quản lí tiến độ, hoặc quản lí chất lượng (đối với dự án quy mô nhỏ)</div><div><br></div><div>III. TRÁCH NHIỆM VÀ NHIỆM VỤ</div><div>1. Làm báo giá dự án Nhật và hỗ trợ giám đốc dự án đi đấu thầu</div><div>• Nắm rõ bản vẽ của dự án từ lúc đấu thầu</div><div>• Hiểu rõ các vật tư spect của ngành kết cấu thép để báo giá</div><div>• Có khả năng bốc khối lượng cho các dự án gấp</div><div>• Có khả năng lên các bản vẽ đề xuất bằng CAD</div><div>• Cùng với GDDA đi đấu thầu các dự án</div><div>• Tiếp khách, đối ứng khách khi có audit, khách về việt nam</div><div><br></div><div>2. Quản lí dự án trúng thầu mảng quản lí bản vẽ thiết kế, quản lí tiến độ</div><div>• Nắm rõ các thay đổi thiết kế, chỉ thị bản vẽ của khách để triển khai cho shop</div><div>• Sử dụng thành thạo Tekla hoặc phần mềm real 4 để xuất các giấy tờ phục vụ cho quản lí dự án</div><div>• Lên được kế hoạch sản xuất và quản lí sản xuất, xuất hàng</div><div><br></div><div>3. Đối ứng khách hàng</div><div>• Báo cáo tiến độ sản xuất, bản vẽ</div><div>• Tham gia chủ đạo trong các cuộc họp tiến độ với nhà máy</div><div><br></div><div>4 . Nghiên cứu &amp; Phát triển</div><div>• Tham gia nghiên cứu và phát triển giải pháp công nghệ sản xuất</div><div><br></div><div>5. Tổng kết và đúc kết kinh nghiệm quản lí dự án</div><div>• Tổng kết thường xuyên các vướng mắt trong tiến độ, bản vẽ . Các lỗi hay mắc phải, hướng xử lí</div><div>• Đối với NCR đóng vai trò chỉ huy xử lí (liên quan tiến độ, chất lượng)</div><div><br></div><div>6. Cost control</div><div>• Phối hợp với giám đốc dự án trong công tác giám sát ngân sách thực hiện</div><div><br></div><div>7. Nhiệm vụ khác</div><div>• Các nhiệm vụ khác được phân công</div>", new DateTime(2023, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "17:30", new DateTime(2023, 4, 22, 15, 17, 18, 794, DateTimeKind.Local).AddTicks(14), 20, 0, 52, 1, "TPHCM - QUẬN 7 - TUYỂN DỤNG GẤP", true, "<div>1. Trình độ đào tạo:</div><div>• Bằng cấp tối thiểu: Cao đẳng, Đại học</div><div>• Chuyên ngành đào tạo: Xây dựng Dân dụng và Công nghiệp, Cơ khí</div><div>• Chứng chỉ, giấy phép hành nghề (nếu có): không yêu cầu.Ưu tiên nếu có chứng chỉ:….</div><div>• Trình độ Ngoại ngữ (tiếng anh, tiếng nhật, tiếng trung): Tiếng Nhật (bắt buộc, N2 đổ lên )</div><div><br></div><div>2. Kiến thức cần có:</div><div>• Sản phẩm dịch vụ của Công ty</div><div>• Kiến thức về lĩnh vực kết cấu thép (tiêu chuẩn, vật liệu, sản xuất, quy trình thi công, lắp dựng)</div><div><br></div><div>3. Năng lực cần thiết:</div><div>Năng lực quản lý</div><div>•Nếu có kinh nghiệm làm ở Nhật, hoặc ở công ty Nhật lâu năm càng tốt</div><div>Năng lực chuyên môn</div><div>•Có bằng cấp liên quan đến kết cấu thép Nhật càng tốt</div><div>•Kỹ năng vẽ CAD cơ bản, Sử dụng thành thạo Tekla hoặc phần mềm real 4</div><div>•Đọc hiểu bản vẽ lắp dựng, bản vẽ tổng thể, tiêu chuẩn đường hàn, tiêu chuẩn JASS 6, ASTM , …</div><div><br></div><div>Năng lực bổ trợ</div><div>• Kỹ năng giao tiếp</div><div>• Kỹ năng giải quyết vấn đề và ra quyết định</div><div>• Kỹ năng làm việc nhóm</div><div>• Kỹ năng sử dụng thành thạo tin học văn phòng</div><div><br></div><div>4. Thái độ hoặc tố chất</div><div>• Có tinh thần trách nhiệm</div><div>• Cẩn thận, tỉ mỉ, chủ động, cầu tiến</div><div><br></div><div>5. Yêu cầu khác:</div><div>•Có đủ sức khỏe để đáp ứng yêu cầu nhiệm vụ được giao</div><div>•Có thể đi công tác</div>", "200000 - 500000", new DateTime(2023, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "8:00", 5, "(CẦN GẤP) Tuyển thợ xây ", true, false, 0, null },
                    { 3, true, "<div>- Tăng lương 6 tháng một lần. Thưởng vào các ngày lễ tết. Cơ hội thăng tiến cao.</div><div>- Xe đưa rước hàng ngày</div><div>- Khám sức khỏe &amp; đi du lịch và các chế độ khác theo quy định của pháp luật Lao động</div>", "Công trình công cộng", 2, new Guid("d7285fb7-835b-4680-a18c-673bd71f63d6"), "<div>I. THÔNG TIN CHUNG</div><div>1. Quy trình công việc liên quan: Quy trình quản lí dự án (mảng bản vẽ, báo giá, tiến độ)</div><div>2. Cấp trực tiếp quản lý: Giám đốc dự án</div><div>3. Loại hợp đồng: Hợp đồng xác định có thời hạn/không thời hạn</div><div><br></div><div>II. MỤC ĐÍCH CÔNG VIỆC</div><div>Nắm bản vẽ của dự án từ lúc đấu thầu, hiểu rõ các spect của dự án để báo giá. Có khả năng bốc khối lượng để đối ứng với cá báo giá gấp, các hạng mục phát sinh. Khi dự án trúng thấu, có khả năng điều phối dự án ở vai trò quản lí thiết kế, quản lí tiến độ, hoặc quản lí chất lượng (đối với dự án quy mô nhỏ)</div><div><br></div><div>III. TRÁCH NHIỆM VÀ NHIỆM VỤ</div><div>1. Làm báo giá dự án Nhật và hỗ trợ giám đốc dự án đi đấu thầu</div><div>• Nắm rõ bản vẽ của dự án từ lúc đấu thầu</div><div>• Hiểu rõ các vật tư spect của ngành kết cấu thép để báo giá</div><div>• Có khả năng bốc khối lượng cho các dự án gấp</div><div>• Có khả năng lên các bản vẽ đề xuất bằng CAD</div><div>• Cùng với GDDA đi đấu thầu các dự án</div><div>• Tiếp khách, đối ứng khách khi có audit, khách về việt nam</div><div><br></div><div>2. Quản lí dự án trúng thầu mảng quản lí bản vẽ thiết kế, quản lí tiến độ</div><div>• Nắm rõ các thay đổi thiết kế, chỉ thị bản vẽ của khách để triển khai cho shop</div><div>• Sử dụng thành thạo Tekla hoặc phần mềm real 4 để xuất các giấy tờ phục vụ cho quản lí dự án</div><div>• Lên được kế hoạch sản xuất và quản lí sản xuất, xuất hàng</div><div><br></div><div>3. Đối ứng khách hàng</div><div>• Báo cáo tiến độ sản xuất, bản vẽ</div><div>• Tham gia chủ đạo trong các cuộc họp tiến độ với nhà máy</div><div><br></div><div>4 . Nghiên cứu &amp; Phát triển</div><div>• Tham gia nghiên cứu và phát triển giải pháp công nghệ sản xuất</div><div><br></div><div>5. Tổng kết và đúc kết kinh nghiệm quản lí dự án</div><div>• Tổng kết thường xuyên các vướng mắt trong tiến độ, bản vẽ . Các lỗi hay mắc phải, hướng xử lí</div><div>• Đối với NCR đóng vai trò chỉ huy xử lí (liên quan tiến độ, chất lượng)</div><div><br></div><div>6. Cost control</div><div>• Phối hợp với giám đốc dự án trong công tác giám sát ngân sách thực hiện</div><div><br></div><div>7. Nhiệm vụ khác</div><div>• Các nhiệm vụ khác được phân công</div>", new DateTime(2023, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "17:30", new DateTime(2023, 4, 22, 15, 17, 18, 794, DateTimeKind.Local).AddTicks(76), 20, 0, 52, 1, "TPHCM - QUẬN 7 - TUYỂN DỤNG GẤP", false, "<div>1. Trình độ đào tạo:</div><div>• Bằng cấp tối thiểu: Cao đẳng, Đại học</div><div>• Chuyên ngành đào tạo: Xây dựng Dân dụng và Công nghiệp, Cơ khí</div><div>• Chứng chỉ, giấy phép hành nghề (nếu có): không yêu cầu.Ưu tiên nếu có chứng chỉ:….</div><div>• Trình độ Ngoại ngữ (tiếng anh, tiếng nhật, tiếng trung): Tiếng Nhật (bắt buộc, N2 đổ lên )</div><div><br></div><div>2. Kiến thức cần có:</div><div>• Sản phẩm dịch vụ của Công ty</div><div>• Kiến thức về lĩnh vực kết cấu thép (tiêu chuẩn, vật liệu, sản xuất, quy trình thi công, lắp dựng)</div><div><br></div><div>3. Năng lực cần thiết:</div><div>Năng lực quản lý</div><div>•Nếu có kinh nghiệm làm ở Nhật, hoặc ở công ty Nhật lâu năm càng tốt</div><div>Năng lực chuyên môn</div><div>•Có bằng cấp liên quan đến kết cấu thép Nhật càng tốt</div><div>•Kỹ năng vẽ CAD cơ bản, Sử dụng thành thạo Tekla hoặc phần mềm real 4</div><div>•Đọc hiểu bản vẽ lắp dựng, bản vẽ tổng thể, tiêu chuẩn đường hàn, tiêu chuẩn JASS 6, ASTM , …</div><div><br></div><div>Năng lực bổ trợ</div><div>• Kỹ năng giao tiếp</div><div>• Kỹ năng giải quyết vấn đề và ra quyết định</div><div>• Kỹ năng làm việc nhóm</div><div>• Kỹ năng sử dụng thành thạo tin học văn phòng</div><div><br></div><div>4. Thái độ hoặc tố chất</div><div>• Có tinh thần trách nhiệm</div><div>• Cẩn thận, tỉ mỉ, chủ động, cầu tiến</div><div><br></div><div>5. Yêu cầu khác:</div><div>•Có đủ sức khỏe để đáp ứng yêu cầu nhiệm vụ được giao</div><div>•Có thể đi công tác</div>", "+600000", new DateTime(2023, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "8:00", 3, "Tuyển Công nhân xây dựng lành nghê ", true, false, 0, null },
                    { 4, false, "<div>- Tăng lương 6 tháng một lần. Thưởng vào các ngày lễ tết. Cơ hội thăng tiến cao.</div><div>- Xe đưa rước hàng ngày</div><div>- Khám sức khỏe &amp; đi du lịch và các chế độ khác theo quy định của pháp luật Lao động</div>", null, 1, new Guid("d7285fb7-835b-4680-a18c-673bd71f63d7"), "<div>I. THÔNG TIN CHUNG</div><div>1. Quy trình công việc liên quan: Quy trình quản lí dự án (mảng bản vẽ, báo giá, tiến độ)</div><div>2. Cấp trực tiếp quản lý: Giám đốc dự án</div><div>3. Loại hợp đồng: Hợp đồng xác định có thời hạn/không thời hạn</div><div><br></div><div>II. MỤC ĐÍCH CÔNG VIỆC</div><div>Nắm bản vẽ của dự án từ lúc đấu thầu, hiểu rõ các spect của dự án để báo giá. Có khả năng bốc khối lượng để đối ứng với cá báo giá gấp, các hạng mục phát sinh. Khi dự án trúng thấu, có khả năng điều phối dự án ở vai trò quản lí thiết kế, quản lí tiến độ, hoặc quản lí chất lượng (đối với dự án quy mô nhỏ)</div><div><br></div><div>III. TRÁCH NHIỆM VÀ NHIỆM VỤ</div><div>1. Làm báo giá dự án Nhật và hỗ trợ giám đốc dự án đi đấu thầu</div><div>• Nắm rõ bản vẽ của dự án từ lúc đấu thầu</div><div>• Hiểu rõ các vật tư spect của ngành kết cấu thép để báo giá</div><div>• Có khả năng bốc khối lượng cho các dự án gấp</div><div>• Có khả năng lên các bản vẽ đề xuất bằng CAD</div><div>• Cùng với GDDA đi đấu thầu các dự án</div><div>• Tiếp khách, đối ứng khách khi có audit, khách về việt nam</div><div><br></div><div>2. Quản lí dự án trúng thầu mảng quản lí bản vẽ thiết kế, quản lí tiến độ</div><div>• Nắm rõ các thay đổi thiết kế, chỉ thị bản vẽ của khách để triển khai cho shop</div><div>• Sử dụng thành thạo Tekla hoặc phần mềm real 4 để xuất các giấy tờ phục vụ cho quản lí dự án</div><div>• Lên được kế hoạch sản xuất và quản lí sản xuất, xuất hàng</div><div><br></div><div>3. Đối ứng khách hàng</div><div>• Báo cáo tiến độ sản xuất, bản vẽ</div><div>• Tham gia chủ đạo trong các cuộc họp tiến độ với nhà máy</div><div><br></div><div>4 . Nghiên cứu &amp; Phát triển</div><div>• Tham gia nghiên cứu và phát triển giải pháp công nghệ sản xuất</div><div><br></div><div>5. Tổng kết và đúc kết kinh nghiệm quản lí dự án</div><div>• Tổng kết thường xuyên các vướng mắt trong tiến độ, bản vẽ . Các lỗi hay mắc phải, hướng xử lí</div><div>• Đối với NCR đóng vai trò chỉ huy xử lí (liên quan tiến độ, chất lượng)</div><div><br></div><div>6. Cost control</div><div>• Phối hợp với giám đốc dự án trong công tác giám sát ngân sách thực hiện</div><div><br></div><div>7. Nhiệm vụ khác</div><div>• Các nhiệm vụ khác được phân công</div>", new DateTime(2023, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2023, 4, 22, 15, 17, 18, 794, DateTimeKind.Local).AddTicks(103), 20, 0, 20, 1, "TPHCM - QUẬN 7 - TUYỂN DỤNG GẤP", false, "<div>1. Trình độ đào tạo:</div><div>• Bằng cấp tối thiểu: Cao đẳng, Đại học</div><div>• Chuyên ngành đào tạo: Xây dựng Dân dụng và Công nghiệp, Cơ khí</div><div>• Chứng chỉ, giấy phép hành nghề (nếu có): không yêu cầu.Ưu tiên nếu có chứng chỉ:….</div><div>• Trình độ Ngoại ngữ (tiếng anh, tiếng nhật, tiếng trung): Tiếng Nhật (bắt buộc, N2 đổ lên )</div><div><br></div><div>2. Kiến thức cần có:</div><div>• Sản phẩm dịch vụ của Công ty</div><div>• Kiến thức về lĩnh vực kết cấu thép (tiêu chuẩn, vật liệu, sản xuất, quy trình thi công, lắp dựng)</div><div><br></div><div>3. Năng lực cần thiết:</div><div>Năng lực quản lý</div><div>•Nếu có kinh nghiệm làm ở Nhật, hoặc ở công ty Nhật lâu năm càng tốt</div><div>Năng lực chuyên môn</div><div>•Có bằng cấp liên quan đến kết cấu thép Nhật càng tốt</div><div>•Kỹ năng vẽ CAD cơ bản, Sử dụng thành thạo Tekla hoặc phần mềm real 4</div><div>•Đọc hiểu bản vẽ lắp dựng, bản vẽ tổng thể, tiêu chuẩn đường hàn, tiêu chuẩn JASS 6, ASTM , …</div><div><br></div><div>Năng lực bổ trợ</div><div>• Kỹ năng giao tiếp</div><div>• Kỹ năng giải quyết vấn đề và ra quyết định</div><div>• Kỹ năng làm việc nhóm</div><div>• Kỹ năng sử dụng thành thạo tin học văn phòng</div><div><br></div><div>4. Thái độ hoặc tố chất</div><div>• Có tinh thần trách nhiệm</div><div>• Cẩn thận, tỉ mỉ, chủ động, cầu tiến</div><div><br></div><div>5. Yêu cầu khác:</div><div>•Có đủ sức khỏe để đáp ứng yêu cầu nhiệm vụ được giao</div><div>•Có thể đi công tác</div>", "+600000", new DateTime(2023, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 3, "Cần người làm dự án (GẤP)", false, false, 0, null },
                    { 5, false, "<div>- Tăng lương 6 tháng một lần. Thưởng vào các ngày lễ tết. Cơ hội thăng tiến cao.</div><div>- Xe đưa rước hàng ngày</div><div>- Khám sức khỏe &amp; đi du lịch và các chế độ khác theo quy định của pháp luật Lao động</div>", null, 2, new Guid("d7285fb7-835b-4680-a18c-673bd71f63d6"), "<div>I. THÔNG TIN CHUNG</div><div>1. Quy trình công việc liên quan: Quy trình quản lí dự án (mảng bản vẽ, báo giá, tiến độ)</div><div>2. Cấp trực tiếp quản lý: Giám đốc dự án</div><div>3. Loại hợp đồng: Hợp đồng xác định có thời hạn/không thời hạn</div><div><br></div><div>II. MỤC ĐÍCH CÔNG VIỆC</div><div>Nắm bản vẽ của dự án từ lúc đấu thầu, hiểu rõ các spect của dự án để báo giá. Có khả năng bốc khối lượng để đối ứng với cá báo giá gấp, các hạng mục phát sinh. Khi dự án trúng thấu, có khả năng điều phối dự án ở vai trò quản lí thiết kế, quản lí tiến độ, hoặc quản lí chất lượng (đối với dự án quy mô nhỏ)</div><div><br></div><div>III. TRÁCH NHIỆM VÀ NHIỆM VỤ</div><div>1. Làm báo giá dự án Nhật và hỗ trợ giám đốc dự án đi đấu thầu</div><div>• Nắm rõ bản vẽ của dự án từ lúc đấu thầu</div><div>• Hiểu rõ các vật tư spect của ngành kết cấu thép để báo giá</div><div>• Có khả năng bốc khối lượng cho các dự án gấp</div><div>• Có khả năng lên các bản vẽ đề xuất bằng CAD</div><div>• Cùng với GDDA đi đấu thầu các dự án</div><div>• Tiếp khách, đối ứng khách khi có audit, khách về việt nam</div><div><br></div><div>2. Quản lí dự án trúng thầu mảng quản lí bản vẽ thiết kế, quản lí tiến độ</div><div>• Nắm rõ các thay đổi thiết kế, chỉ thị bản vẽ của khách để triển khai cho shop</div><div>• Sử dụng thành thạo Tekla hoặc phần mềm real 4 để xuất các giấy tờ phục vụ cho quản lí dự án</div><div>• Lên được kế hoạch sản xuất và quản lí sản xuất, xuất hàng</div><div><br></div><div>3. Đối ứng khách hàng</div><div>• Báo cáo tiến độ sản xuất, bản vẽ</div><div>• Tham gia chủ đạo trong các cuộc họp tiến độ với nhà máy</div><div><br></div><div>4 . Nghiên cứu &amp; Phát triển</div><div>• Tham gia nghiên cứu và phát triển giải pháp công nghệ sản xuất</div><div><br></div><div>5. Tổng kết và đúc kết kinh nghiệm quản lí dự án</div><div>• Tổng kết thường xuyên các vướng mắt trong tiến độ, bản vẽ . Các lỗi hay mắc phải, hướng xử lí</div><div>• Đối với NCR đóng vai trò chỉ huy xử lí (liên quan tiến độ, chất lượng)</div><div><br></div><div>6. Cost control</div><div>• Phối hợp với giám đốc dự án trong công tác giám sát ngân sách thực hiện</div><div><br></div><div>7. Nhiệm vụ khác</div><div>• Các nhiệm vụ khác được phân công</div>", new DateTime(2024, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2023, 4, 22, 15, 17, 18, 794, DateTimeKind.Local).AddTicks(135), 30, 0, 14, 2, "TPHCM - QUẬN 7 - TUYỂN DỤNG GẤP", false, "<div>1. Trình độ đào tạo:</div><div>• Bằng cấp tối thiểu: Cao đẳng, Đại học</div><div>• Chuyên ngành đào tạo: Xây dựng Dân dụng và Công nghiệp, Cơ khí</div><div>• Chứng chỉ, giấy phép hành nghề (nếu có): không yêu cầu.Ưu tiên nếu có chứng chỉ:….</div><div>• Trình độ Ngoại ngữ (tiếng anh, tiếng nhật, tiếng trung): Tiếng Nhật (bắt buộc, N2 đổ lên )</div><div><br></div><div>2. Kiến thức cần có:</div><div>• Sản phẩm dịch vụ của Công ty</div><div>• Kiến thức về lĩnh vực kết cấu thép (tiêu chuẩn, vật liệu, sản xuất, quy trình thi công, lắp dựng)</div><div><br></div><div>3. Năng lực cần thiết:</div><div>Năng lực quản lý</div><div>•Nếu có kinh nghiệm làm ở Nhật, hoặc ở công ty Nhật lâu năm càng tốt</div><div>Năng lực chuyên môn</div><div>•Có bằng cấp liên quan đến kết cấu thép Nhật càng tốt</div><div>•Kỹ năng vẽ CAD cơ bản, Sử dụng thành thạo Tekla hoặc phần mềm real 4</div><div>•Đọc hiểu bản vẽ lắp dựng, bản vẽ tổng thể, tiêu chuẩn đường hàn, tiêu chuẩn JASS 6, ASTM , …</div><div><br></div><div>Năng lực bổ trợ</div><div>• Kỹ năng giao tiếp</div><div>• Kỹ năng giải quyết vấn đề và ra quyết định</div><div>• Kỹ năng làm việc nhóm</div><div>• Kỹ năng sử dụng thành thạo tin học văn phòng</div><div><br></div><div>4. Thái độ hoặc tố chất</div><div>• Có tinh thần trách nhiệm</div><div>• Cẩn thận, tỉ mỉ, chủ động, cầu tiến</div><div><br></div><div>5. Yêu cầu khác:</div><div>•Có đủ sức khỏe để đáp ứng yêu cầu nhiệm vụ được giao</div><div>•Có thể đi công tác</div>", "+600000", new DateTime(2023, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 3, "Tuyển dụng công nhân xây dựng", false, false, 0, null },
                    { 6, false, "<div>- Tăng lương 6 tháng một lần. Thưởng vào các ngày lễ tết. Cơ hội thăng tiến cao.</div><div>- Xe đưa rước hàng ngày</div><div>- Khám sức khỏe &amp; đi du lịch và các chế độ khác theo quy định của pháp luật Lao động</div>", null, 1, new Guid("d7285fb7-835b-4680-a18c-673bd71f63d7"), "<div>I. THÔNG TIN CHUNG</div><div>1. Quy trình công việc liên quan: Quy trình quản lí dự án (mảng bản vẽ, báo giá, tiến độ)</div><div>2. Cấp trực tiếp quản lý: Giám đốc dự án</div><div>3. Loại hợp đồng: Hợp đồng xác định có thời hạn/không thời hạn</div><div><br></div><div>II. MỤC ĐÍCH CÔNG VIỆC</div><div>Nắm bản vẽ của dự án từ lúc đấu thầu, hiểu rõ các spect của dự án để báo giá. Có khả năng bốc khối lượng để đối ứng với cá báo giá gấp, các hạng mục phát sinh. Khi dự án trúng thấu, có khả năng điều phối dự án ở vai trò quản lí thiết kế, quản lí tiến độ, hoặc quản lí chất lượng (đối với dự án quy mô nhỏ)</div><div><br></div><div>III. TRÁCH NHIỆM VÀ NHIỆM VỤ</div><div>1. Làm báo giá dự án Nhật và hỗ trợ giám đốc dự án đi đấu thầu</div><div>• Nắm rõ bản vẽ của dự án từ lúc đấu thầu</div><div>• Hiểu rõ các vật tư spect của ngành kết cấu thép để báo giá</div><div>• Có khả năng bốc khối lượng cho các dự án gấp</div><div>• Có khả năng lên các bản vẽ đề xuất bằng CAD</div><div>• Cùng với GDDA đi đấu thầu các dự án</div><div>• Tiếp khách, đối ứng khách khi có audit, khách về việt nam</div><div><br></div><div>2. Quản lí dự án trúng thầu mảng quản lí bản vẽ thiết kế, quản lí tiến độ</div><div>• Nắm rõ các thay đổi thiết kế, chỉ thị bản vẽ của khách để triển khai cho shop</div><div>• Sử dụng thành thạo Tekla hoặc phần mềm real 4 để xuất các giấy tờ phục vụ cho quản lí dự án</div><div>• Lên được kế hoạch sản xuất và quản lí sản xuất, xuất hàng</div><div><br></div><div>3. Đối ứng khách hàng</div><div>• Báo cáo tiến độ sản xuất, bản vẽ</div><div>• Tham gia chủ đạo trong các cuộc họp tiến độ với nhà máy</div><div><br></div><div>4 . Nghiên cứu &amp; Phát triển</div><div>• Tham gia nghiên cứu và phát triển giải pháp công nghệ sản xuất</div><div><br></div><div>5. Tổng kết và đúc kết kinh nghiệm quản lí dự án</div><div>• Tổng kết thường xuyên các vướng mắt trong tiến độ, bản vẽ . Các lỗi hay mắc phải, hướng xử lí</div><div>• Đối với NCR đóng vai trò chỉ huy xử lí (liên quan tiến độ, chất lượng)</div><div><br></div><div>6. Cost control</div><div>• Phối hợp với giám đốc dự án trong công tác giám sát ngân sách thực hiện</div><div><br></div><div>7. Nhiệm vụ khác</div><div>• Các nhiệm vụ khác được phân công</div>", new DateTime(2024, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2023, 4, 22, 15, 17, 18, 794, DateTimeKind.Local).AddTicks(162), 30, 0, 14, 2, "TPHCM - QUẬN 7 - TUYỂN DỤNG GẤP", false, "<div>1. Trình độ đào tạo:</div><div>• Bằng cấp tối thiểu: Cao đẳng, Đại học</div><div>• Chuyên ngành đào tạo: Xây dựng Dân dụng và Công nghiệp, Cơ khí</div><div>• Chứng chỉ, giấy phép hành nghề (nếu có): không yêu cầu.Ưu tiên nếu có chứng chỉ:….</div><div>• Trình độ Ngoại ngữ (tiếng anh, tiếng nhật, tiếng trung): Tiếng Nhật (bắt buộc, N2 đổ lên )</div><div><br></div><div>2. Kiến thức cần có:</div><div>• Sản phẩm dịch vụ của Công ty</div><div>• Kiến thức về lĩnh vực kết cấu thép (tiêu chuẩn, vật liệu, sản xuất, quy trình thi công, lắp dựng)</div><div><br></div><div>3. Năng lực cần thiết:</div><div>Năng lực quản lý</div><div>•Nếu có kinh nghiệm làm ở Nhật, hoặc ở công ty Nhật lâu năm càng tốt</div><div>Năng lực chuyên môn</div><div>•Có bằng cấp liên quan đến kết cấu thép Nhật càng tốt</div><div>•Kỹ năng vẽ CAD cơ bản, Sử dụng thành thạo Tekla hoặc phần mềm real 4</div><div>•Đọc hiểu bản vẽ lắp dựng, bản vẽ tổng thể, tiêu chuẩn đường hàn, tiêu chuẩn JASS 6, ASTM , …</div><div><br></div><div>Năng lực bổ trợ</div><div>• Kỹ năng giao tiếp</div><div>• Kỹ năng giải quyết vấn đề và ra quyết định</div><div>• Kỹ năng làm việc nhóm</div><div>• Kỹ năng sử dụng thành thạo tin học văn phòng</div><div><br></div><div>4. Thái độ hoặc tố chất</div><div>• Có tinh thần trách nhiệm</div><div>• Cẩn thận, tỉ mỉ, chủ động, cầu tiến</div><div><br></div><div>5. Yêu cầu khác:</div><div>•Có đủ sức khỏe để đáp ứng yêu cầu nhiệm vụ được giao</div><div>•Có thể đi công tác</div>", "+600000", new DateTime(2023, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 3, "Tuyển dụng công nhân xây dựng", false, false, 0, null },
                    { 7, true, "<div>- Tăng lương 6 tháng một lần. Thưởng vào các ngày lễ tết. Cơ hội thăng tiến cao.</div><div>- Xe đưa rước hàng ngày</div><div>- Khám sức khỏe &amp; đi du lịch và các chế độ khác theo quy định của pháp luật Lao động</div>", "Nhà ở", 1, new Guid("d7285fb7-835b-4680-a18c-673bd71f63d7"), "<div>I. THÔNG TIN CHUNG</div><div>1. Quy trình công việc liên quan: Quy trình quản lí dự án (mảng bản vẽ, báo giá, tiến độ)</div><div>2. Cấp trực tiếp quản lý: Giám đốc dự án</div><div>3. Loại hợp đồng: Hợp đồng xác định có thời hạn/không thời hạn</div><div><br></div><div>II. MỤC ĐÍCH CÔNG VIỆC</div><div>Nắm bản vẽ của dự án từ lúc đấu thầu, hiểu rõ các spect của dự án để báo giá. Có khả năng bốc khối lượng để đối ứng với cá báo giá gấp, các hạng mục phát sinh. Khi dự án trúng thấu, có khả năng điều phối dự án ở vai trò quản lí thiết kế, quản lí tiến độ, hoặc quản lí chất lượng (đối với dự án quy mô nhỏ)</div><div><br></div><div>III. TRÁCH NHIỆM VÀ NHIỆM VỤ</div><div>1. Làm báo giá dự án Nhật và hỗ trợ giám đốc dự án đi đấu thầu</div><div>• Nắm rõ bản vẽ của dự án từ lúc đấu thầu</div><div>• Hiểu rõ các vật tư spect của ngành kết cấu thép để báo giá</div><div>• Có khả năng bốc khối lượng cho các dự án gấp</div><div>• Có khả năng lên các bản vẽ đề xuất bằng CAD</div><div>• Cùng với GDDA đi đấu thầu các dự án</div><div>• Tiếp khách, đối ứng khách khi có audit, khách về việt nam</div><div><br></div><div>2. Quản lí dự án trúng thầu mảng quản lí bản vẽ thiết kế, quản lí tiến độ</div><div>• Nắm rõ các thay đổi thiết kế, chỉ thị bản vẽ của khách để triển khai cho shop</div><div>• Sử dụng thành thạo Tekla hoặc phần mềm real 4 để xuất các giấy tờ phục vụ cho quản lí dự án</div><div>• Lên được kế hoạch sản xuất và quản lí sản xuất, xuất hàng</div><div><br></div><div>3. Đối ứng khách hàng</div><div>• Báo cáo tiến độ sản xuất, bản vẽ</div><div>• Tham gia chủ đạo trong các cuộc họp tiến độ với nhà máy</div><div><br></div><div>4 . Nghiên cứu &amp; Phát triển</div><div>• Tham gia nghiên cứu và phát triển giải pháp công nghệ sản xuất</div><div><br></div><div>5. Tổng kết và đúc kết kinh nghiệm quản lí dự án</div><div>• Tổng kết thường xuyên các vướng mắt trong tiến độ, bản vẽ . Các lỗi hay mắc phải, hướng xử lí</div><div>• Đối với NCR đóng vai trò chỉ huy xử lí (liên quan tiến độ, chất lượng)</div><div><br></div><div>6. Cost control</div><div>• Phối hợp với giám đốc dự án trong công tác giám sát ngân sách thực hiện</div><div><br></div><div>7. Nhiệm vụ khác</div><div>• Các nhiệm vụ khác được phân công</div>", new DateTime(2023, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "16:30", new DateTime(2023, 4, 22, 15, 17, 18, 794, DateTimeKind.Local).AddTicks(205), 20, 0, 52, 1, "TPHCM - QUẬN 10 - TUYỂN DỤNG GẤP", false, "<div>1. Trình độ đào tạo:</div><div>• Bằng cấp tối thiểu: Cao đẳng, Đại học</div><div>• Chuyên ngành đào tạo: Xây dựng Dân dụng và Công nghiệp, Cơ khí</div><div>• Chứng chỉ, giấy phép hành nghề (nếu có): không yêu cầu.Ưu tiên nếu có chứng chỉ:….</div><div>• Trình độ Ngoại ngữ (tiếng anh, tiếng nhật, tiếng trung): Tiếng Nhật (bắt buộc, N2 đổ lên )</div><div><br></div><div>2. Kiến thức cần có:</div><div>• Sản phẩm dịch vụ của Công ty</div><div>• Kiến thức về lĩnh vực kết cấu thép (tiêu chuẩn, vật liệu, sản xuất, quy trình thi công, lắp dựng)</div><div><br></div><div>3. Năng lực cần thiết:</div><div>Năng lực quản lý</div><div>•Nếu có kinh nghiệm làm ở Nhật, hoặc ở công ty Nhật lâu năm càng tốt</div><div>Năng lực chuyên môn</div><div>•Có bằng cấp liên quan đến kết cấu thép Nhật càng tốt</div><div>•Kỹ năng vẽ CAD cơ bản, Sử dụng thành thạo Tekla hoặc phần mềm real 4</div><div>•Đọc hiểu bản vẽ lắp dựng, bản vẽ tổng thể, tiêu chuẩn đường hàn, tiêu chuẩn JASS 6, ASTM , …</div><div><br></div><div>Năng lực bổ trợ</div><div>• Kỹ năng giao tiếp</div><div>• Kỹ năng giải quyết vấn đề và ra quyết định</div><div>• Kỹ năng làm việc nhóm</div><div>• Kỹ năng sử dụng thành thạo tin học văn phòng</div><div><br></div><div>4. Thái độ hoặc tố chất</div><div>• Có tinh thần trách nhiệm</div><div>• Cẩn thận, tỉ mỉ, chủ động, cầu tiến</div><div><br></div><div>5. Yêu cầu khác:</div><div>•Có đủ sức khỏe để đáp ứng yêu cầu nhiệm vụ được giao</div><div>•Có thể đi công tác</div>", "+600000", new DateTime(2023, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "9:00", 3, "Tuyển dụng nam lao động xây dựng cao cấp", true, false, 0, null },
                    { 8, true, "<div>- Tăng lương 6 tháng một lần. Thưởng vào các ngày lễ tết. Cơ hội thăng tiến cao.</div><div>- Xe đưa rước hàng ngày</div><div>- Khám sức khỏe &amp; đi du lịch và các chế độ khác theo quy định của pháp luật Lao động</div>", "Nhà ở", 1, new Guid("d7285fb7-835b-4680-a18c-673bd71f63d7"), "<div>I. THÔNG TIN CHUNG</div><div>1. Quy trình công việc liên quan: Quy trình quản lí dự án (mảng bản vẽ, báo giá, tiến độ)</div><div>2. Cấp trực tiếp quản lý: Giám đốc dự án</div><div>3. Loại hợp đồng: Hợp đồng xác định có thời hạn/không thời hạn</div><div><br></div><div>II. MỤC ĐÍCH CÔNG VIỆC</div><div>Nắm bản vẽ của dự án từ lúc đấu thầu, hiểu rõ các spect của dự án để báo giá. Có khả năng bốc khối lượng để đối ứng với cá báo giá gấp, các hạng mục phát sinh. Khi dự án trúng thấu, có khả năng điều phối dự án ở vai trò quản lí thiết kế, quản lí tiến độ, hoặc quản lí chất lượng (đối với dự án quy mô nhỏ)</div><div><br></div><div>III. TRÁCH NHIỆM VÀ NHIỆM VỤ</div><div>1. Làm báo giá dự án Nhật và hỗ trợ giám đốc dự án đi đấu thầu</div><div>• Nắm rõ bản vẽ của dự án từ lúc đấu thầu</div><div>• Hiểu rõ các vật tư spect của ngành kết cấu thép để báo giá</div><div>• Có khả năng bốc khối lượng cho các dự án gấp</div><div>• Có khả năng lên các bản vẽ đề xuất bằng CAD</div><div>• Cùng với GDDA đi đấu thầu các dự án</div><div>• Tiếp khách, đối ứng khách khi có audit, khách về việt nam</div><div><br></div><div>2. Quản lí dự án trúng thầu mảng quản lí bản vẽ thiết kế, quản lí tiến độ</div><div>• Nắm rõ các thay đổi thiết kế, chỉ thị bản vẽ của khách để triển khai cho shop</div><div>• Sử dụng thành thạo Tekla hoặc phần mềm real 4 để xuất các giấy tờ phục vụ cho quản lí dự án</div><div>• Lên được kế hoạch sản xuất và quản lí sản xuất, xuất hàng</div><div><br></div><div>3. Đối ứng khách hàng</div><div>• Báo cáo tiến độ sản xuất, bản vẽ</div><div>• Tham gia chủ đạo trong các cuộc họp tiến độ với nhà máy</div><div><br></div><div>4 . Nghiên cứu &amp; Phát triển</div><div>• Tham gia nghiên cứu và phát triển giải pháp công nghệ sản xuất</div><div><br></div><div>5. Tổng kết và đúc kết kinh nghiệm quản lí dự án</div><div>• Tổng kết thường xuyên các vướng mắt trong tiến độ, bản vẽ . Các lỗi hay mắc phải, hướng xử lí</div><div>• Đối với NCR đóng vai trò chỉ huy xử lí (liên quan tiến độ, chất lượng)</div><div><br></div><div>6. Cost control</div><div>• Phối hợp với giám đốc dự án trong công tác giám sát ngân sách thực hiện</div><div><br></div><div>7. Nhiệm vụ khác</div><div>• Các nhiệm vụ khác được phân công</div>", new DateTime(2023, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "16:30", new DateTime(2023, 4, 22, 15, 17, 18, 794, DateTimeKind.Local).AddTicks(277), 20, 0, 52, 2, "TPHCM - TUYỂN DỤNG GẤP", false, "<div>1. Trình độ đào tạo:</div><div>• Bằng cấp tối thiểu: Cao đẳng, Đại học</div><div>• Chuyên ngành đào tạo: Xây dựng Dân dụng và Công nghiệp, Cơ khí</div><div>• Chứng chỉ, giấy phép hành nghề (nếu có): không yêu cầu.Ưu tiên nếu có chứng chỉ:….</div><div>• Trình độ Ngoại ngữ (tiếng anh, tiếng nhật, tiếng trung): Tiếng Nhật (bắt buộc, N2 đổ lên )</div><div><br></div><div>2. Kiến thức cần có:</div><div>• Sản phẩm dịch vụ của Công ty</div><div>• Kiến thức về lĩnh vực kết cấu thép (tiêu chuẩn, vật liệu, sản xuất, quy trình thi công, lắp dựng)</div><div><br></div><div>3. Năng lực cần thiết:</div><div>Năng lực quản lý</div><div>•Nếu có kinh nghiệm làm ở Nhật, hoặc ở công ty Nhật lâu năm càng tốt</div><div>Năng lực chuyên môn</div><div>•Có bằng cấp liên quan đến kết cấu thép Nhật càng tốt</div><div>•Kỹ năng vẽ CAD cơ bản, Sử dụng thành thạo Tekla hoặc phần mềm real 4</div><div>•Đọc hiểu bản vẽ lắp dựng, bản vẽ tổng thể, tiêu chuẩn đường hàn, tiêu chuẩn JASS 6, ASTM , …</div><div><br></div><div>Năng lực bổ trợ</div><div>• Kỹ năng giao tiếp</div><div>• Kỹ năng giải quyết vấn đề và ra quyết định</div><div>• Kỹ năng làm việc nhóm</div><div>• Kỹ năng sử dụng thành thạo tin học văn phòng</div><div><br></div><div>4. Thái độ hoặc tố chất</div><div>• Có tinh thần trách nhiệm</div><div>• Cẩn thận, tỉ mỉ, chủ động, cầu tiến</div><div><br></div><div>5. Yêu cầu khác:</div><div>•Có đủ sức khỏe để đáp ứng yêu cầu nhiệm vụ được giao</div><div>•Có thể đi công tác</div>", "+600000", new DateTime(2023, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "9:00", 3, "Tuyển dụng nam lao động ", true, false, 0, null },
                    { 9, true, "<div>- Tăng lương 6 tháng một lần. Thưởng vào các ngày lễ tết. Cơ hội thăng tiến cao.</div><div>- Xe đưa rước hàng ngày</div><div>- Khám sức khỏe &amp; đi du lịch và các chế độ khác theo quy định của pháp luật Lao động</div>", "Nhà ở", 1, new Guid("d7285fb7-835b-4680-a18c-673bd71f63d7"), "<div>I. THÔNG TIN CHUNG</div><div>1. Quy trình công việc liên quan: Quy trình quản lí dự án (mảng bản vẽ, báo giá, tiến độ)</div><div>2. Cấp trực tiếp quản lý: Giám đốc dự án</div><div>3. Loại hợp đồng: Hợp đồng xác định có thời hạn/không thời hạn</div><div><br></div><div>II. MỤC ĐÍCH CÔNG VIỆC</div><div>Nắm bản vẽ của dự án từ lúc đấu thầu, hiểu rõ các spect của dự án để báo giá. Có khả năng bốc khối lượng để đối ứng với cá báo giá gấp, các hạng mục phát sinh. Khi dự án trúng thấu, có khả năng điều phối dự án ở vai trò quản lí thiết kế, quản lí tiến độ, hoặc quản lí chất lượng (đối với dự án quy mô nhỏ)</div><div><br></div><div>III. TRÁCH NHIỆM VÀ NHIỆM VỤ</div><div>1. Làm báo giá dự án Nhật và hỗ trợ giám đốc dự án đi đấu thầu</div><div>• Nắm rõ bản vẽ của dự án từ lúc đấu thầu</div><div>• Hiểu rõ các vật tư spect của ngành kết cấu thép để báo giá</div><div>• Có khả năng bốc khối lượng cho các dự án gấp</div><div>• Có khả năng lên các bản vẽ đề xuất bằng CAD</div><div>• Cùng với GDDA đi đấu thầu các dự án</div><div>• Tiếp khách, đối ứng khách khi có audit, khách về việt nam</div><div><br></div><div>2. Quản lí dự án trúng thầu mảng quản lí bản vẽ thiết kế, quản lí tiến độ</div><div>• Nắm rõ các thay đổi thiết kế, chỉ thị bản vẽ của khách để triển khai cho shop</div><div>• Sử dụng thành thạo Tekla hoặc phần mềm real 4 để xuất các giấy tờ phục vụ cho quản lí dự án</div><div>• Lên được kế hoạch sản xuất và quản lí sản xuất, xuất hàng</div><div><br></div><div>3. Đối ứng khách hàng</div><div>• Báo cáo tiến độ sản xuất, bản vẽ</div><div>• Tham gia chủ đạo trong các cuộc họp tiến độ với nhà máy</div><div><br></div><div>4 . Nghiên cứu &amp; Phát triển</div><div>• Tham gia nghiên cứu và phát triển giải pháp công nghệ sản xuất</div><div><br></div><div>5. Tổng kết và đúc kết kinh nghiệm quản lí dự án</div><div>• Tổng kết thường xuyên các vướng mắt trong tiến độ, bản vẽ . Các lỗi hay mắc phải, hướng xử lí</div><div>• Đối với NCR đóng vai trò chỉ huy xử lí (liên quan tiến độ, chất lượng)</div><div><br></div><div>6. Cost control</div><div>• Phối hợp với giám đốc dự án trong công tác giám sát ngân sách thực hiện</div><div><br></div><div>7. Nhiệm vụ khác</div><div>• Các nhiệm vụ khác được phân công</div>", new DateTime(2023, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "16:30", new DateTime(2023, 4, 22, 15, 17, 18, 794, DateTimeKind.Local).AddTicks(323), 20, 0, 52, 2, "TPHCM - TUYỂN DỤNG GẤP", false, "<div>1. Trình độ đào tạo:</div><div>• Bằng cấp tối thiểu: Cao đẳng, Đại học</div><div>• Chuyên ngành đào tạo: Xây dựng Dân dụng và Công nghiệp, Cơ khí</div><div>• Chứng chỉ, giấy phép hành nghề (nếu có): không yêu cầu.Ưu tiên nếu có chứng chỉ:….</div><div>• Trình độ Ngoại ngữ (tiếng anh, tiếng nhật, tiếng trung): Tiếng Nhật (bắt buộc, N2 đổ lên )</div><div><br></div><div>2. Kiến thức cần có:</div><div>• Sản phẩm dịch vụ của Công ty</div><div>• Kiến thức về lĩnh vực kết cấu thép (tiêu chuẩn, vật liệu, sản xuất, quy trình thi công, lắp dựng)</div><div><br></div><div>3. Năng lực cần thiết:</div><div>Năng lực quản lý</div><div>•Nếu có kinh nghiệm làm ở Nhật, hoặc ở công ty Nhật lâu năm càng tốt</div><div>Năng lực chuyên môn</div><div>•Có bằng cấp liên quan đến kết cấu thép Nhật càng tốt</div><div>•Kỹ năng vẽ CAD cơ bản, Sử dụng thành thạo Tekla hoặc phần mềm real 4</div><div>•Đọc hiểu bản vẽ lắp dựng, bản vẽ tổng thể, tiêu chuẩn đường hàn, tiêu chuẩn JASS 6, ASTM , …</div><div><br></div><div>Năng lực bổ trợ</div><div>• Kỹ năng giao tiếp</div><div>• Kỹ năng giải quyết vấn đề và ra quyết định</div><div>• Kỹ năng làm việc nhóm</div><div>• Kỹ năng sử dụng thành thạo tin học văn phòng</div><div><br></div><div>4. Thái độ hoặc tố chất</div><div>• Có tinh thần trách nhiệm</div><div>• Cẩn thận, tỉ mỉ, chủ động, cầu tiến</div><div><br></div><div>5. Yêu cầu khác:</div><div>•Có đủ sức khỏe để đáp ứng yêu cầu nhiệm vụ được giao</div><div>•Có thể đi công tác</div>", "+600000", new DateTime(2023, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "9:00", 3, "Tuyển dụng xây dựng", true, false, 0, null },
                    { 10, true, "<div>- Tăng lương 6 tháng một lần. Thưởng vào các ngày lễ tết. Cơ hội thăng tiến cao.</div><div>- Xe đưa rước hàng ngày</div><div>- Khám sức khỏe &amp; đi du lịch và các chế độ khác theo quy định của pháp luật Lao động</div>", "Nhà ở", 1, new Guid("d7285fb7-835b-4680-a18c-673bd71f63d7"), "<div>I. THÔNG TIN CHUNG</div><div>1. Quy trình công việc liên quan: Quy trình quản lí dự án (mảng bản vẽ, báo giá, tiến độ)</div><div>2. Cấp trực tiếp quản lý: Giám đốc dự án</div><div>3. Loại hợp đồng: Hợp đồng xác định có thời hạn/không thời hạn</div><div><br></div><div>II. MỤC ĐÍCH CÔNG VIỆC</div><div>Nắm bản vẽ của dự án từ lúc đấu thầu, hiểu rõ các spect của dự án để báo giá. Có khả năng bốc khối lượng để đối ứng với cá báo giá gấp, các hạng mục phát sinh. Khi dự án trúng thấu, có khả năng điều phối dự án ở vai trò quản lí thiết kế, quản lí tiến độ, hoặc quản lí chất lượng (đối với dự án quy mô nhỏ)</div><div><br></div><div>III. TRÁCH NHIỆM VÀ NHIỆM VỤ</div><div>1. Làm báo giá dự án Nhật và hỗ trợ giám đốc dự án đi đấu thầu</div><div>• Nắm rõ bản vẽ của dự án từ lúc đấu thầu</div><div>• Hiểu rõ các vật tư spect của ngành kết cấu thép để báo giá</div><div>• Có khả năng bốc khối lượng cho các dự án gấp</div><div>• Có khả năng lên các bản vẽ đề xuất bằng CAD</div><div>• Cùng với GDDA đi đấu thầu các dự án</div><div>• Tiếp khách, đối ứng khách khi có audit, khách về việt nam</div><div><br></div><div>2. Quản lí dự án trúng thầu mảng quản lí bản vẽ thiết kế, quản lí tiến độ</div><div>• Nắm rõ các thay đổi thiết kế, chỉ thị bản vẽ của khách để triển khai cho shop</div><div>• Sử dụng thành thạo Tekla hoặc phần mềm real 4 để xuất các giấy tờ phục vụ cho quản lí dự án</div><div>• Lên được kế hoạch sản xuất và quản lí sản xuất, xuất hàng</div><div><br></div><div>3. Đối ứng khách hàng</div><div>• Báo cáo tiến độ sản xuất, bản vẽ</div><div>• Tham gia chủ đạo trong các cuộc họp tiến độ với nhà máy</div><div><br></div><div>4 . Nghiên cứu &amp; Phát triển</div><div>• Tham gia nghiên cứu và phát triển giải pháp công nghệ sản xuất</div><div><br></div><div>5. Tổng kết và đúc kết kinh nghiệm quản lí dự án</div><div>• Tổng kết thường xuyên các vướng mắt trong tiến độ, bản vẽ . Các lỗi hay mắc phải, hướng xử lí</div><div>• Đối với NCR đóng vai trò chỉ huy xử lí (liên quan tiến độ, chất lượng)</div><div><br></div><div>6. Cost control</div><div>• Phối hợp với giám đốc dự án trong công tác giám sát ngân sách thực hiện</div><div><br></div><div>7. Nhiệm vụ khác</div><div>• Các nhiệm vụ khác được phân công</div>", new DateTime(2023, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "16:30", new DateTime(2023, 4, 22, 15, 17, 18, 794, DateTimeKind.Local).AddTicks(436), 20, 0, 52, 2, "TPHCM - TUYỂN DỤNG GẤP", false, "<div>1. Trình độ đào tạo:</div><div>• Bằng cấp tối thiểu: Cao đẳng, Đại học</div><div>• Chuyên ngành đào tạo: Xây dựng Dân dụng và Công nghiệp, Cơ khí</div><div>• Chứng chỉ, giấy phép hành nghề (nếu có): không yêu cầu.Ưu tiên nếu có chứng chỉ:….</div><div>• Trình độ Ngoại ngữ (tiếng anh, tiếng nhật, tiếng trung): Tiếng Nhật (bắt buộc, N2 đổ lên )</div><div><br></div><div>2. Kiến thức cần có:</div><div>• Sản phẩm dịch vụ của Công ty</div><div>• Kiến thức về lĩnh vực kết cấu thép (tiêu chuẩn, vật liệu, sản xuất, quy trình thi công, lắp dựng)</div><div><br></div><div>3. Năng lực cần thiết:</div><div>Năng lực quản lý</div><div>•Nếu có kinh nghiệm làm ở Nhật, hoặc ở công ty Nhật lâu năm càng tốt</div><div>Năng lực chuyên môn</div><div>•Có bằng cấp liên quan đến kết cấu thép Nhật càng tốt</div><div>•Kỹ năng vẽ CAD cơ bản, Sử dụng thành thạo Tekla hoặc phần mềm real 4</div><div>•Đọc hiểu bản vẽ lắp dựng, bản vẽ tổng thể, tiêu chuẩn đường hàn, tiêu chuẩn JASS 6, ASTM , …</div><div><br></div><div>Năng lực bổ trợ</div><div>• Kỹ năng giao tiếp</div><div>• Kỹ năng giải quyết vấn đề và ra quyết định</div><div>• Kỹ năng làm việc nhóm</div><div>• Kỹ năng sử dụng thành thạo tin học văn phòng</div><div><br></div><div>4. Thái độ hoặc tố chất</div><div>• Có tinh thần trách nhiệm</div><div>• Cẩn thận, tỉ mỉ, chủ động, cầu tiến</div><div><br></div><div>5. Yêu cầu khác:</div><div>•Có đủ sức khỏe để đáp ứng yêu cầu nhiệm vụ được giao</div><div>•Có thể đi công tác</div>", "+600000", new DateTime(2023, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "9:00", 3, "Tuyển dụng xây dựng ", true, false, 0, null }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Brand", "CreateBy", "CreatedBy", "Description", "Image", "LastModifiedAt", "MaterialStoreID", "Name", "SoldQuantities", "Status", "Unit", "UnitInStock", "UnitPrice" },
                values: new object[,]
                {
                    { 1, "Pháp", new Guid("00000000-0000-0000-0000-000000000000"), null, "Ngói lợp kiểu Pháp cổ điển", "https://sbo.vn/wp-content/uploads/2021/06/tam-lop-sinh-thai-onduline.jpg", new DateTime(2023, 4, 22, 15, 17, 18, 794, DateTimeKind.Local).AddTicks(1402), 1, "Ngói lợp", 3000, true, "ton", 300000, 700m },
                    { 2, "Việt Nam", new Guid("00000000-0000-0000-0000-000000000000"), null, "Gạch 2 lỗ cao cấp đến từ thương hiệu nổi tiếng ", "http://www.phudien.vn/upload/Product%20400x200/G%E1%BA%A1ch%20tuynel%20-%20g%E1%BA%A1ch%206%20l%E1%BB%97%20lo%E1%BA%A1i%20nh%E1%BB%8F.png", new DateTime(2023, 4, 22, 15, 17, 18, 794, DateTimeKind.Local).AddTicks(1412), 1, "Gạch lỗ", 1500, true, "ton", 300000, 40000m },
                    { 3, "Mỹ", new Guid("00000000-0000-0000-0000-000000000000"), null, "Sơn chống thấm Nippon", "https://nipponpaint.com.vn/sites/default/files/inline-images/son-chong-tham-la-gi-1.jpg", new DateTime(2023, 4, 22, 15, 17, 18, 794, DateTimeKind.Local).AddTicks(1422), 1, "Sơn chống thấm", 1500, true, "ton", 2000, 1000000m },
                    { 4, "Việt Nam", new Guid("00000000-0000-0000-0000-000000000000"), null, "Cát mịn dành cho xây dựng đặc biệt dành cho ngôi nhà yêu dấu của bạn", "https://sbshouse.vn/wp-content/uploads/2020/09/cat-xay-dung.jpg", new DateTime(2023, 4, 22, 15, 17, 18, 794, DateTimeKind.Local).AddTicks(1435), 1, "Cát Mịn", 50, true, "ton", 200000, 50000m },
                    { 5, "Koreana", new Guid("00000000-0000-0000-0000-000000000000"), null, "Miễn phí vận chuyển, lắp đặp tại Hà Nội & HCM", "https://vuongquocnoithat.vn/images/2016/09/22/phong-ngu-phong-cach-cong-chua-jy921.jpg", new DateTime(2023, 4, 22, 15, 17, 18, 794, DateTimeKind.Local).AddTicks(1446), 2, "Giường ngủ công chúa", 300, true, "cái", 3, 30000000m },
                    { 6, "AUSTRIAN LAMP", new Guid("00000000-0000-0000-0000-000000000000"), null, "Nhập khẩu 100%\r\n\r\n -Bảo hành 2 năm\r\n\r\n -Miễn phí vận chuyển, lắp đặp tại Hà Nội & HCMShowroom HN: 3000m2 Tầng 1&2, tòa T2", "https://vuongquocnoithat.vn/images/2018/01/29/den-chum-dong-co-dien-kieu-italia%20atl8501.jpg", new DateTime(2023, 4, 22, 15, 17, 18, 794, DateTimeKind.Local).AddTicks(1506), 2, "Đèn chùm đồng cổ điển phong cách Ý", 1, true, "cái", 3, 2000000m },
                    { 7, "CMC", new Guid("00000000-0000-0000-0000-000000000000"), null, "- Màng sơn mịn có độ phủ cao, siêu bóng sang trọng,bám dính tốt\r\n\r\n- Hạn chế vết bẩn, vết nứt nhỏ, chống rêu mốc, độ bền màu cao\r\n\r\n- Thân thiện môi trường và an toàn cho sức khỏe\r\n\r\n- Bảo vệ 10 năm\r\n\r\n- Độ phủ lý thuyết: 12-14m2/lít/ lớp", "https://admin.mingstores.com/core/public/themes/mingstores/products/vx9kXzl3FacoKvdbZLki3kWM6nO3PimJ.jpg", new DateTime(2022, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Sơn Ngoại Thất Bóng Cao Cấp CMC ARMOS07 1 - 4.5L", 1500, true, "Lít", 200, 857000m },
                    { 20, "KANSAI PAINT", new Guid("00000000-0000-0000-0000-000000000000"), null, "- Sơn chống thấm Một thành phần Aqua Shield\r\n\r\n- Chống thấm tuyệt hảo\r\n\r\n- Kháng nước tuyệt đối\r\n\r\n- Che phủ vết nứt, co giãn tốt, dễ thi công (không chứa xi măng)\r\n\r\n", "https://admin.mingstores.com/core/public/themes/mingstores/products/OOUUL3p3xO6kV63bOCyr4qCMZBNDo2yc.jpg", new DateTime(2023, 4, 22, 15, 17, 18, 794, DateTimeKind.Local).AddTicks(1565), 1, "K023 Sơn Kansai chống thấm Aqua Shield 5L, 18L", 100, true, "Lít", 1000, 960000m },
                    { 21, "INAX", new Guid("00000000-0000-0000-0000-000000000000"), null, "Sen cây nóng lạnh INAX BFV-515S là sản phẩm sen cây INAX  được thiết kế tay sen cài liền cùng thân sen cây thay vì để gắn tường, giúp cho tổng thể bộ sen cây trở nên gọn gàng, linh hoạt, đặc biệt phù hợp cả với những căn phòng tắm kích thước nhỏ, quý khách hàng vẫn có thể lắp đặt mẫu sen cây này và cảm nhận trải nghiệm khác biệt khi tắm vòi sen cây với bát sen lớn.\r\nMẫu thiết kế sen cây thuộc dòng sản phẩm SEN VÒI INAX có thiết kế đẹp mắt, sáng tạo từ kiểu dáng đến tính năng thích hợp cho mọi loại hình phòng tắm từ những phòng tắm đơn giản, nhỏ hẹp, đến những căn phòng tắm hiện đại, tiện nghi. ", "https://admin.mingstores.com/core/public/themes/mingstores/products/Mjzhtin7lD3gCUXksET0srIdUnABPNE3.jpg", new DateTime(2023, 4, 22, 15, 17, 18, 794, DateTimeKind.Local).AddTicks(1711), 1, "SEN TẮM CÂY INAX BFV-515S", 1000, true, "Cái", 1200, 12485000m },
                    { 22, "INAX", new Guid("00000000-0000-0000-0000-000000000000"), null, "Là mẫu chậu rửa mặt Inax đặt bàn mới nhất 2017, sản phẩm tiêu biểu cho năm 2018", "https://admin.mingstores.com/core/public/themes/mingstores/products/JnyguIQW8EMvvUqcZ6BZnGSLOeL5OgpK.jpg", new DateTime(2023, 4, 22, 15, 17, 18, 794, DateTimeKind.Local).AddTicks(1749), 1, "Chậu Rửa Lavabo Inax AL-536V", 100, true, "Cái", 100, 2046000m },
                    { 30, "NIRO GRANITE", new Guid("00000000-0000-0000-0000-000000000000"), null, "Gạch cao cấp đến từ thương hiệu nổi tiếng NIRO GRANITE", "https://admin.mingstores.com/core/public/themes/mingstores/products/Elgda4SYGE52gAn2wi5AEXipIEMqiYiB.jpg", new DateTime(2022, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Gạch GCA-Clay Art 60x60", 1500, true, "Viên", 200, 400000m },
                    { 31, "ARISTON", new Guid("00000000-0000-0000-0000-000000000000"), null, "Công suất định mức: 4500(W)\r\nHình dáng: Hình tròn\r\nĐiện năng: 220V\r\nChế độ vòi sen: 5\r\nÁp lực nước tối thiểu: 30/0,3 Kpa/bar\r\nÁp lực nước tối đa: 380/3.8 Kpa/bar\r\nKích thước (DxCxR): 350 X 80\r\nTrọng lượng: 2 kg\r\nKhông có bơm trợ lực", "https://admin.mingstores.com/core/public/themes/mingstores/products/MbcC070BBSf4q97sgghpjBbqFiNr7JEP.jpg", new DateTime(2023, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Aures Smart Round RMC-45E-VN", 1500, true, "Bộ", 200, 3200000m },
                    { 32, "TOTO", new Guid("00000000-0000-0000-0000-000000000000"), null, "Thiết kế nguyên khối sang trọng, hiện đại\r\nNắp bàn cầu đóng êm, kèm vòi rửa nước lạnh Eco-washer\r\nBề mặt nước rộng giúp ngăn mùi hiệu quả\r\nThiết kế thân kín, vành kín tiện dụng cho việc vệ sinh hàng ngày\r\nCông nghệ CeFiONtect giúp lòng bàn cầu siêu nhẵn, hạn chế tối đa các vết bẩn, vi khuẩn\r\nCông nghệ xả G-Max êm, mạnh mẽ hiệu quả", "https://admin.mingstores.com/core/public/themes/mingstores/products/UYZ61ie7Z7i5Hmjd6D7XyUWhBZVL7y8v.jpg", new DateTime(2023, 4, 22, 15, 17, 18, 794, DateTimeKind.Local).AddTicks(862), 1, "Bồn cầu một khối TOTO MS904E4", 1500, true, "Bộ", 200, 19037000m },
                    { 33, "INAX", new Guid("00000000-0000-0000-0000-000000000000"), null, "Dòng sản phẩm bồn cầu INAX AC-1017R 1 khối cao cấp đến từ thương hiệu thiết bị vệ sinh INAX\r\nBệt Inax AC-1017R CW-KA22AVN 1 khối với thiết kế mới đơn giản, gọn gàng và sang trọng hơn kết hợp với những tính năng cải tiến\r\nCông nghệ ECO-X xã xoáy cuốn trôi mọi vết bẩn\r\nCông nghệ Aqua Ceramic giúp bề mạt men sứ trắng sáng trong suốt thời gian sữ dụng\r\nCông nghệ chống khuẩn HYPERKILAMIC kháng khuẩn độc quyền của INAX Nhật Bản. \r\nE-Clean: Chức năng phun rửa  tự động\r\nEvaClean: Chức năng vệ sinh phụ nữ\r\nCozyCare: Chức năng sưởi ấm bệ ngồi\r\nX-Fresh: Chức năng khử mùi nhanh \r\nEcoPower: Chức năng tiết kiệm điện “1 lần chạm” (8 tiếng sau tự khôi phục)\r\nDung tích két nước nóng: 0.67L (lít) Vòi phun rửa:\r\nVòi phun rửa và vòi phun dùng riêng cho phụ nữ đều là loại trượt tự động.  \r\nThiết bị an toàn: Rơ-le nhiệt, cảm ứng từ kiểm soát nhiệt độ cao, phao ngắt để thiết bị ngừng hoạt động khi không đủ nước, cảm ứng tự ngắt khi gặp sự cố. \r\nNước cấp: Nối trực tiếp từ đường ống nước ", "https://admin.mingstores.com/core/public/themes/mingstores/products/ryBdemssBcpSt6vbeQdirRUMcBszbZKt.jpg", new DateTime(2022, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Bồn Cầu Thông Minh INAX AC-1017R/CW-KA22AVN", 1500, true, "Bộ", 200, 26301000m },
                    { 34, "NIRO GRANITE", new Guid("00000000-0000-0000-0000-000000000000"), null, "Màu sắc: vàng, xám, trắng, đen\r\nKích thước: 30x60, 60x60", "https://admin.mingstores.com/core/public/themes/mingstores/products/U3SmJsX6rBhkAPyQ3Xym2wlyoNTH6pGz.jpg", new DateTime(2023, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "GFS Gạch Granite Fusion Bán Bóng/Sần", 1500, true, "Cái", 200, 520000m },
                    { 35, "JINMEI", new Guid("00000000-0000-0000-0000-000000000000"), null, "ông nghệ sản xuất tủ lavabo của chúng tôi đã được chuyên nghiệp hóa qua nhiều năm phát triển, với phần khung bên ngoài được làm bằng nhôm, là cấu trúc chính hỗ trợ, giúp toàn bộ tủ chắc chắn, bên cạnh phần bản lề được làm bằng INOX 304 dày, giúp cho việc vận hành được trơn tru, ổn định.\r\n- Các bộ phận chính đều được làm bằng thép không gỉ 304 (INOX 304), tăng độ bền cho sản phẩm trong quá trình sử dụng.\r\n- Cấu hình cạnh và tay nắm cửa được làm bằng máy vát 45 độ đặc biệt, góc nhôm được gắn chặt vào thành bên trong tủ, để bề mặt sản phẩm mịn & tinh tế, tạo sự thoải mái khi sử dụng.\r\n\r\n- Việc sử dụng nhôm để làm vật liệu chính sản xuất tủ Lavabo là lựa chọn tối ưu nhất hiện nay, không chỉ có độ bền cao, nhôm hoàn toàn không độc hại với môi trường cũng như người sử dụng. Một số ưu điểm chính của nhôm:\r\n  + Trọng lượng nhẹ, độ bền cao, khả năng chịu lực lớn.\r\n  + Độ cứng tốt, không dễ biến dạng.\r\n  + Không thấm nước trong môi trường có độ ẩm cao, không bắt lửa và chịu được tác động mạnh.\r\n  + Lớp sơn phủ bền màu, chống ăn mòn do thời tiết hoặc hóa chất thông thường.\r\n  + Tạo không gian sang trọng, thoải mái và tiện lợi.", "https://admin.mingstores.com/core/public/themes/mingstores/products/P5GERhsHMvYboHFoSTcetoIuHKJJApvD.jpg", new DateTime(2023, 4, 22, 15, 17, 18, 794, DateTimeKind.Local).AddTicks(1171), 1, "Tủ Lavabo JM843", 1500, true, "Bộ", 200, 9520000m },
                    { 36, "KANSAI PAINT", new Guid("00000000-0000-0000-0000-000000000000"), null, "Sơn ngoại thất câo cấp đến từ thương hiệu Kansai nổi tiếng", "https://admin.mingstores.com/core/public/themes/mingstores/products/JnLYt6lx4OLgmoplQoxTPU1e9SBjZf9a.jpg", new DateTime(2023, 4, 22, 15, 17, 18, 794, DateTimeKind.Local).AddTicks(1213), 1, "K015 Sơn Kansai chống thấm Water Proof 4L, 17L", 1500, true, "Lít", 200, 750000m },
                    { 37, "COSMOS", new Guid("00000000-0000-0000-0000-000000000000"), null, "KARI SQUARE STEP LIGHT (3W) mang đến ánh sáng tinh tế đến gia đình bạn ", "https://admin.mingstores.com/core/public/themes/mingstores/products/N23c1P48S9v157cAYlNRc35gS92VeYVE.jpg", new DateTime(2023, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "KARI SQUARE STEP LIGHT (3W)", 1500, true, "Cái", 200, 460000m },
                    { 38, "NIRO GRANITE", new Guid("00000000-0000-0000-0000-000000000000"), null, "GHR Gạch Granite Hardrock Mờ/Bán bóng ", "https://admin.mingstores.com/core/public/themes/mingstores/products/6TauBDiJiwnvQaJTuCl9D0SYHFayTRHk.jpg", new DateTime(2023, 4, 22, 15, 17, 18, 794, DateTimeKind.Local).AddTicks(1366), 1, "GHR Gạch Granite Hardrock Mờ/Bán bóng", 1500, true, "Viên", 200, 360000m }
                });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "FromSystem", "Name", "TypeId" },
                values: new object[,]
                {
                    { 1, true, "Xây dựng", new Guid("4ace8fcb-95eb-48c0-9deb-240e8b4e10e0") },
                    { 2, true, "Sơn tường", new Guid("ce9fa65b-d005-46b6-953e-e6462a59cfb3") },
                    { 3, true, "Lát gạch", new Guid("bd880489-5c76-4854-93ab-66e3a541bf24") },
                    { 4, true, "Thạch Cao", new Guid("ce9fa65b-d005-46b6-953e-e6462a59cfb3") },
                    { 5, true, "Điện", new Guid("ce9fa65b-d005-46b6-953e-e6462a59cfb3") },
                    { 6, true, "Nước", new Guid("ce9fa65b-d005-46b6-953e-e6462a59cfb3") },
                    { 7, true, "Hàn Sắt", new Guid("ce9fa65b-d005-46b6-953e-e6462a59cfb3") },
                    { 8, true, "Ốp lát", new Guid("ce9fa65b-d005-46b6-953e-e6462a59cfb3") }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "Address", "Avatar", "WorkerID", "ConcurrencyStamp", "ContractorId", "CreateBy", "DOB", "Email", "EmailConfirmed", "FirstName", "Gender", "IdNumber", "LastModifiedAt", "LastName", "LockoutEnabled", "LockoutEnd", "MaterialStoreID", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Provider", "RefreshTokenExpiryTime", "SecurityStamp", "Status", "Token", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("86b8070e-00c5-45de-8db7-199cee7350d9"), 0, "56 Nguyễn Duy Trinh, Huyện Mỏ Cày Nam, Bến Tre", "https://diaocthinhvuong.vn/wp-content/uploads/2021/05/1logo-newtecons.jpg", null, "137e862d-eec5-458b-baf7-0cd817d4d571", 3, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2001, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "contractor3@gmail.com", false, "Công ty TNHH ", 0, null, new DateTime(2023, 4, 22, 15, 17, 18, 764, DateTimeKind.Local).AddTicks(7273), "Ánh Nhiên Xanh", false, null, null, null, null, "AQAAAAEAACcQAAAAEId+qi9VHSgC2P3VmbcvbCdbg3bjvWW+PeVxQ++Fb1qSJe5XwG6qGktg140AqcP2zQ==", "0888694499", false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 3, "xxx", false, "0987654321" },
                    { new Guid("b57b172a-a044-11ed-a8fc-0242ac120002"), 0, "56 Nguyễn Duy Trinh, Huyện An Lão, Bình Định", "https://baodautu.vn/Images/chicong/2018/11/28/thi-truong-vat-lieu-xay-dung-mua-kinh-doanh-da-thay-doi1543390455.jpg", null, "53ba681d-99f8-4e13-8401-11c129c8ced8", null, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2001, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "store123@gmail.com", false, "TPHCM", 0, null, new DateTime(2023, 4, 22, 15, 17, 18, 774, DateTimeKind.Local).AddTicks(3051), "Cửa Hàng Vật Liệu", false, null, 1, null, null, "AQAAAAEAACcQAAAAEISqhwQIFmjWADNaV/wqRUtF0ujo45tZ3qS1k8WHl58GkZSkvI8THDzK9EbWPLWdRg==", "0924516734", false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 3, "xxx", false, "0924516734" },
                    { new Guid("be21b564-a044-11ed-a8fc-0242ac120002"), 0, "56 Nguyễn Duy Trinh, Huyện Hàm Tân, Bình Thuận", "https://thaicong.com/wp-content/uploads/2017/11/img_sub_4.jpg", null, "f81fde85-872d-4590-a216-5f5696a84a9d", null, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2001, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "store2@gmail.com", false, "Cần Thơ", 0, null, new DateTime(2023, 4, 22, 15, 17, 18, 784, DateTimeKind.Local).AddTicks(402), "VLXD", false, null, 2, null, null, "AQAAAAEAACcQAAAAEB2Vm0hvF6cJe1gYQyUZoMgoOpDgf71JbckCPCoB6s6oTOMaER0ztBP0KnSfjIfWTQ==", "09245167342", false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 3, "xxx", false, "09245167342" },
                    { new Guid("d7285fb7-835b-4680-a18c-673bd71f63d6"), 0, "56 Nguyễn Duy Trinh, Huyện Mỏ Cày Nam, Bến Tre", "https://www.vietnamworks.com/_next/image?url=https%3A%2F%2Fimages.vietnamworks.com%2Fpictureofcompany%2F69%2F11128477.png&w=128&q=75", null, "d6040f77-aeac-4d09-95cc-4ff4dc3bcc6c", 2, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2001, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "contractor2@gmail.com", false, "Công Ty Cổ Phần Đầu Tư Bất Động Sản", 0, null, new DateTime(2023, 4, 22, 15, 17, 18, 755, DateTimeKind.Local).AddTicks(442), "Taseco", false, null, null, null, null, "AQAAAAEAACcQAAAAELegWVkUbQJTGV6eHEGYi2VimiODlh4Rv+/5gc8zDXZ0MPNRN+IggfOtjYRmrWJwYw==", "09987654321", false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 3, "xxx", false, "0987654321" },
                    { new Guid("d7285fb7-835b-4680-a18c-673bd71f63d7"), 0, "56 Nguyễn Duy Trinh, Huyện Gia Bình, Bắc Ninh", "https://www.vietnamworks.com/_next/image?url=https%3A%2F%2Fimages.vietnamworks.com%2Fpictureofcompany%2F78%2F11127264.png&w=128&q=75", null, "43cacaf4-06f0-4b0f-80a6-20cf388e80ac", 1, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2001, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "contractor@gmail.com", false, "Công Ty Cổ Phần Xây Dựng Và Công Nghiệp", 0, null, new DateTime(2023, 4, 22, 15, 17, 18, 745, DateTimeKind.Local).AddTicks(3069), "NSN", false, null, null, null, null, "AQAAAAEAACcQAAAAEDN9+8mPZ+YjpjSBwGzGRz0lwdabdefJiScyuv3AStv7HZJVp49EhSu3BACH9a2sJw==", "0912345678", false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 3, "xxx", false, "0912345678" }
                });

            migrationBuilder.InsertData(
                table: "Workers",
                columns: new[] { "Id", "Certificate", "ConstructionType", "CreateBy", "Experience", "ExperienceDetail", "Image", "LastModifiedAt", "Place", "TypeID" },
                values: new object[,]
                {
                    { 1, "Chứng chỉ a;công ty a;09/04/2011;https://i1.rgstatic.net/publication/311457103_Certificate_of_Design_Builder_Training/links/58480cfb08aeda696825d727/largepreview.png|Chứng chỉ b;công ty b;09/04/2011;https://i1.rgstatic.net/publication/311457103_Certificate_of_Design_Builder_Training/links/58480cfb08aeda696825d727/largepreview.png", null, new Guid("d7285fb7-835b-4680-a18c-673bd71f63d9"), 3, null, null, new DateTime(2023, 4, 22, 15, 17, 18, 688, DateTimeKind.Local).AddTicks(9172), 60, new Guid("4ace8fcb-95eb-48c0-9deb-240e8b4e10e0") },
                    { 2, "Chứng chỉ a;công ty a;09/04/2011;https://i1.rgstatic.net/publication/311457103_Certificate_of_Design_Builder_Training/links/58480cfb08aeda696825d727/largepreview.png|Chứng chỉ b;công ty b;09/04/2011;https://i1.rgstatic.net/publication/311457103_Certificate_of_Design_Builder_Training/links/58480cfb08aeda696825d727/largepreview.png", null, new Guid("d39ae0a6-9b2d-4421-be4a-cc294cec054f"), 3, null, null, new DateTime(2023, 4, 22, 15, 17, 18, 699, DateTimeKind.Local).AddTicks(7282), 61, new Guid("bd880489-5c76-4854-93ab-66e3a541bf24") },
                    { 3, "Chứng chỉ a;công ty a;09/04/2011;https://i1.rgstatic.net/publication/311457103_Certificate_of_Design_Builder_Training/links/58480cfb08aeda696825d727/largepreview.png|Chứng chỉ b;công ty b;09/04/2011;https://i1.rgstatic.net/publication/311457103_Certificate_of_Design_Builder_Training/links/58480cfb08aeda696825d727/largepreview.png", null, new Guid("d91f9ece-25a7-4dc6-adde-186b12c04d56"), 1, null, null, new DateTime(2023, 4, 22, 15, 17, 18, 712, DateTimeKind.Local).AddTicks(6493), 16, new Guid("ce9fa65b-d005-46b6-953e-e6462a59cfb3") },
                    { 4, "Chứng chỉ a;công ty a;09/04/2011;https://i1.rgstatic.net/publication/311457103_Certificate_of_Design_Builder_Training/links/58480cfb08aeda696825d727/largepreview.png|Chứng chỉ b;công ty b;09/04/2011;https://i1.rgstatic.net/publication/311457103_Certificate_of_Design_Builder_Training/links/58480cfb08aeda696825d727/largepreview.png", null, new Guid("319d2a06-92cc-434d-abce-7e8a33650a0d"), 4, null, null, new DateTime(2023, 4, 22, 15, 17, 18, 725, DateTimeKind.Local).AddTicks(8257), 52, new Guid("ce9fa65b-d005-46b6-953e-e6462a59cfb3") },
                    { 5, "Chứng chỉ a;công ty a;09/04/2011;https://i1.rgstatic.net/publication/311457103_Certificate_of_Design_Builder_Training/links/58480cfb08aeda696825d727/largepreview.png|Chứng chỉ b;công ty b;09/04/2011;https://i1.rgstatic.net/publication/311457103_Certificate_of_Design_Builder_Training/links/58480cfb08aeda696825d727/largepreview.png", null, new Guid("8f314589-0c7c-40a4-b5bc-c73639664922"), 1, null, null, new DateTime(2023, 4, 22, 15, 17, 18, 735, DateTimeKind.Local).AddTicks(6232), 42, new Guid("cf9fa65b-d005-46b6-953e-e6462a59cfb3") },
                    { 6, "Chứng chỉ a;công ty a;09/04/2011;https://i1.rgstatic.net/publication/311457103_Certificate_of_Design_Builder_Training/links/58480cfb08aeda696825d727/largepreview.png|Chứng chỉ b;công ty b;09/04/2011;https://i1.rgstatic.net/publication/311457103_Certificate_of_Design_Builder_Training/links/58480cfb08aeda696825d727/largepreview.png", null, new Guid("17c76dfe-7a0b-4ac9-ab8b-ba95e588a135"), 2, null, null, new DateTime(2023, 4, 22, 15, 17, 18, 745, DateTimeKind.Local).AddTicks(2957), 13, new Guid("cf9fa65b-d005-46b6-953e-e6462a59cfb3") }
                });

            migrationBuilder.InsertData(
                table: "AppliedPost",
                columns: new[] { "WorkerID", "PostID", "AppliedDate", "GroupID", "QuizId", "Status", "Video", "WishSalary" },
                values: new object[,]
                {
                    { 1, 3, new DateTime(2023, 4, 22, 15, 17, 18, 794, DateTimeKind.Local).AddTicks(1908), null, null, 6, null, null },
                    { 2, 4, new DateTime(2023, 4, 22, 15, 17, 18, 794, DateTimeKind.Local).AddTicks(1921), null, null, 6, null, null },
                    { 3, 4, new DateTime(2023, 4, 22, 15, 17, 18, 794, DateTimeKind.Local).AddTicks(1931), null, null, 6, null, null }
                });

            migrationBuilder.InsertData(
                table: "ContractorPostSkills",
                columns: new[] { "ContractorPostID", "SkillID" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 5, 1 },
                    { 7, 1 },
                    { 8, 1 },
                    { 9, 1 },
                    { 10, 1 },
                    { 1, 2 },
                    { 4, 2 },
                    { 7, 2 },
                    { 2, 3 },
                    { 3, 3 },
                    { 4, 3 },
                    { 6, 4 },
                    { 2, 5 }
                });

            migrationBuilder.InsertData(
                table: "ContractorPostType",
                columns: new[] { "ContractorPostID", "TypeID" },
                values: new object[,]
                {
                    { 1, new Guid("4ace8fcb-95eb-48c0-9deb-240e8b4e10e0") },
                    { 2, new Guid("4ace8fcb-95eb-48c0-9deb-240e8b4e10e0") },
                    { 7, new Guid("4ace8fcb-95eb-48c0-9deb-240e8b4e10e0") },
                    { 8, new Guid("4ace8fcb-95eb-48c0-9deb-240e8b4e10e0") },
                    { 9, new Guid("4ace8fcb-95eb-48c0-9deb-240e8b4e10e0") },
                    { 10, new Guid("4ace8fcb-95eb-48c0-9deb-240e8b4e10e0") },
                    { 1, new Guid("ce9fa65b-d005-46b6-953e-e6462a59cfb3") },
                    { 7, new Guid("ce9fa65b-d005-46b6-953e-e6462a59cfb3") },
                    { 8, new Guid("ce9fa65b-d005-46b6-953e-e6462a59cfb3") },
                    { 9, new Guid("ce9fa65b-d005-46b6-953e-e6462a59cfb3") },
                    { 10, new Guid("ce9fa65b-d005-46b6-953e-e6462a59cfb3") }
                });

            migrationBuilder.InsertData(
                table: "Notifications",
                columns: new[] { "Id", "CreateBy", "IsRead", "LastModifiedAt", "Message", "NavigateId", "Title", "Type", "UserID" },
                values: new object[,]
                {
                    { 1, new Guid("d7285fb7-835b-4680-a18c-673bd71f63d9"), false, new DateTime(2023, 4, 22, 15, 17, 18, 794, DateTimeKind.Local).AddTicks(1804), "Someone has saved your post", 1, "New Notification", 0, new Guid("d7285fb7-835b-4680-a18c-673bd71f63d7") },
                    { 2, new Guid("d7285fb7-835b-4680-a18c-673bd71f63d9"), false, new DateTime(2023, 4, 22, 15, 17, 18, 794, DateTimeKind.Local).AddTicks(1821), "Someone has applied your post", 1, "New Notification", 0, new Guid("d7285fb7-835b-4680-a18c-673bd71f63d7") },
                    { 3, new Guid("d7285fb7-835b-4680-a18c-673bd71f63d9"), false, new DateTime(2023, 4, 22, 15, 17, 18, 794, DateTimeKind.Local).AddTicks(1833), "Create commitment successfully", 1, "New Notification", 0, new Guid("d7285fb7-835b-4680-a18c-673bd71f63d7") }
                });

            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "CategoriesID", "ProductID", "Name" },
                values: new object[,]
                {
                    { 2, 5, "Gỗ tự nhiên" },
                    { 2, 6, "Đồng" },
                    { 1, 7, "Mỹ" },
                    { 2, 7, "Sơn ngoại thất" },
                    { 1, 20, "Niro – Indonesia" },
                    { 2, 20, "Lít" },
                    { 4, 20, "Sơn ngoại thất" },
                    { 1, 21, "Nhật Bản" },
                    { 2, 21, "Cái" },
                    { 4, 21, "Nhà tắm" }
                });

            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "CategoriesID", "ProductID", "Name" },
                values: new object[,]
                {
                    { 1, 22, "Nhật Bản" },
                    { 2, 22, "Cái" },
                    { 4, 22, "Nhà tắm" },
                    { 1, 30, "Mỹ" },
                    { 2, 30, "Gạch bóng" },
                    { 1, 31, "Mỹ" },
                    { 2, 31, "Thép" },
                    { 1, 32, "Mỹ" },
                    { 2, 32, "Cái" },
                    { 3, 32, "Hiện đại" },
                    { 4, 32, "Nhà vệ sinh" },
                    { 1, 33, "Mỹ" },
                    { 2, 33, "Cái" },
                    { 3, 33, "Cổ điển" },
                    { 4, 33, "Nhà vệ sinh" },
                    { 1, 34, "Niro – Indonesia" },
                    { 2, 34, "Cái" },
                    { 1, 35, "Niro – Indonesia" },
                    { 2, 35, "Bộ" },
                    { 4, 35, "Nhà tắm" },
                    { 1, 36, "Ý" },
                    { 2, 36, "Bộ" },
                    { 4, 36, "Sơn nội thất" },
                    { 1, 37, "Ý" },
                    { 2, 37, "Bộ" },
                    { 4, 37, "Phòng khách" },
                    { 1, 38, "Niro – Indonesia" },
                    { 2, 38, "Cái" },
                    { 4, 38, "Nội thất" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "ColorId", "Label", "OtherID", "ProductID", "Quantity", "SizeID", "Status" },
                values: new object[,]
                {
                    { 12, 1, null, 1, 7, 5, 2, 3 },
                    { 13, 1, null, 1, 7, 10, 3, 3 },
                    { 50, 6, null, 1, 34, 5, 4, 3 },
                    { 51, 6, null, 1, 34, 3, 5, 3 },
                    { 52, 7, null, 1, 34, 5, 4, 3 },
                    { 53, 7, null, 1, 34, 5, 5, 3 },
                    { 60, null, null, null, 6, 3, 10, 3 },
                    { 61, null, null, null, 6, 3, 11, 3 },
                    { 63, null, null, null, 5, 1, 14, 3 },
                    { 64, null, null, null, 5, 2, 15, 3 },
                    { 65, 1, null, 1, 20, 5, 7, 3 },
                    { 66, 1, null, 1, 20, 3, 8, 3 }
                });

            migrationBuilder.InsertData(
                table: "Quiz",
                columns: new[] { "Id", "LastModifiedAt", "Name", "PostID", "TypeID" },
                values: new object[] { 1, new DateTime(2023, 4, 22, 15, 17, 18, 794, DateTimeKind.Local).AddTicks(2082), "Bài test thợ xây ", 1, new Guid("4ace8fcb-95eb-48c0-9deb-240e8b4e10e0") });

            migrationBuilder.InsertData(
                table: "Quiz",
                columns: new[] { "Id", "LastModifiedAt", "Name", "PostID", "TypeID" },
                values: new object[] { 2, new DateTime(2023, 4, 22, 15, 17, 18, 794, DateTimeKind.Local).AddTicks(2304), "Bài test thợ sơn ", 1, new Guid("ce9fa65b-d005-46b6-953e-e6462a59cfb3") });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "Address", "Avatar", "WorkerID", "ConcurrencyStamp", "ContractorId", "CreateBy", "DOB", "Email", "EmailConfirmed", "FirstName", "Gender", "IdNumber", "LastModifiedAt", "LastName", "LockoutEnabled", "LockoutEnd", "MaterialStoreID", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Provider", "RefreshTokenExpiryTime", "SecurityStamp", "Status", "Token", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("17c76dfe-7a0b-4ac9-ab8b-ba95e588a135"), 0, "135 Nguyễn Hiếu, Huyện Chợ Đồn, Bắc Kạn", "https://demoda.vn/wp-content/uploads/2022/03/mau-anh-the-ong-chu-so-mi-trang.jpg", 6, "6e52b28d-7d71-4065-b862-8af75e438f71", null, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2001, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "namhoaidoan15@gmail.com", false, "Dương", 0, null, new DateTime(2023, 4, 22, 15, 17, 18, 735, DateTimeKind.Local).AddTicks(6511), "Thanh Vàng", false, null, null, null, null, "AQAAAAEAACcQAAAAEIZoZPapq7m1oWrYqiffPHTFuKPeehfBpS7tT76kCzdwvvKu5nfDH1O+NbeahQDMlw==", "0404040404", false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 3, "xxx", false, "namhoaidoan15@gmail.com" },
                    { new Guid("319d2a06-92cc-434d-abce-7e8a33650a0d"), 0, "56 Nguyễn Duy Trinh, Huyện Chợ Đồn, Bắc Kạn", "https://upload.wikimedia.org/wikipedia/commons/b/b3/%E1%BA%A2nh_ch%C3%A2n_dung_Nguy%E1%BB%85n_V%C4%83n_Minh_Tr%C3%AD.jpg", 4, "2342dd88-09e9-4635-b2e8-db4f1e5eb444", null, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2001, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "namhoaidoan13@gmail.com", false, "Minh", 0, null, new DateTime(2023, 4, 22, 15, 17, 18, 712, DateTimeKind.Local).AddTicks(6635), "Nguyen Trần", false, null, null, null, null, "AQAAAAEAACcQAAAAENUTZrBg5VDm/QKVWsNpnIoLNwZ3ARrpg9tKHYJACkWqlFDwpQ5plNk6pUyE+y/mEg==", "0202020202", false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 3, "xxx", false, "namhoaidoan13@gmail.com" },
                    { new Guid("8f314589-0c7c-40a4-b5bc-c73639664922"), 0, "135 Nguyễn Hiếu, Huyện Chợ Đồn, Bắc Kạn", "https://upload.wikimedia.org/wikipedia/commons/1/10/%E1%BA%A2nh-th%E1%BA%BB-v%C6%B0%E1%BB%A3ng.png", 5, "019855f9-75ff-4249-85a2-3539ebbc3845", null, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2001, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "namhoaidoan14@gmail.com", false, "Trúc", 0, null, new DateTime(2023, 4, 22, 15, 17, 18, 725, DateTimeKind.Local).AddTicks(8424), "Phạm Thanh", false, null, null, null, null, "AQAAAAEAACcQAAAAEAOYF6N56G/cB1v67upgxIWaJ8wh8i1BqTPUjBp4dGsB06Ayw04rhSzhY04iLFN63A==", "0303030303", false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 3, "xxx", false, "namhoaidoan14@gmail.com" },
                    { new Guid("d39ae0a6-9b2d-4421-be4a-cc294cec054f"), 0, "41 Nguyễn Duy Trinh, Huyện Tân Yên, Bắc Giang", "https://scontent.fsgn5-3.fna.fbcdn.net/v/t1.6435-9/86186750_1329130013936346_7257030880831471616_n.jpg?_nc_cat=104&ccb=1-7&_nc_sid=174925&_nc_ohc=Z1GTPvzRt7wAX_WbRZ5&_nc_ht=scontent.fsgn5-3.fna&oh=00_AfAYtaD2dHE_84_-PSlDqLaeyBlH9zJ3b308pHcTWucCXw&oe=642552F2", 2, "e984eaa4-5192-42c4-8605-2a526faf0d5e", null, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2001, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "namhoaidoan1@gmail.com", false, "Thinh", 0, null, new DateTime(2023, 4, 22, 15, 17, 18, 688, DateTimeKind.Local).AddTicks(9362), "Nguyen Anh", false, null, null, null, null, "AQAAAAEAACcQAAAAEH/WVoxUoUnV8Y54qNx+6GKox1q9JTEZNBPKNu6YwDLM9oqKLUMvz3JpaG0yXrzwng==", "0937341639", false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 3, "xxx", false, "namhoaidoan1@gmail.com" },
                    { new Guid("d7285fb7-835b-4680-a18c-673bd71f63d9"), 0, "41 Nguyễn Duy Trinh, Huyện Đông Hải, Bạc Liêu", "https://i1-giaitri.vnecdn.net/2013/08/15/DK-02756-1376528749.jpg?w=680&h=0&q=100&dpr=1&fit=crop&s=mX89l0q4HQgntQ5wJesOcw", 1, "881e959f-36f6-4635-81bb-43e353e8ee24", null, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2001, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "namhoaidoan15@gmail.com", false, "Hoai Nam", 0, null, new DateTime(2023, 4, 22, 15, 17, 18, 677, DateTimeKind.Local).AddTicks(4060), "Doan Vu", false, null, null, null, null, "AQAAAAEAACcQAAAAEAbmFyG/0QdyRn3F7i2PntN4V5ZILQRkPEzjXzAlLtDbTPXL1SaXLVqOZtmmZZz68A==", "0879411575", false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 3, "xxx", false, "namhoaidoan15@gmail.com" },
                    { new Guid("d91f9ece-25a7-4dc6-adde-186b12c04d56"), 0, "56 Nguyễn Duy Trinh, Huyện Chợ Đồn, Bắc Kạn", "https://scontent.fsgn5-3.fna.fbcdn.net/v/t1.6435-9/86186750_1329130013936346_7257030880831471616_n.jpg?_nc_cat=104&ccb=1-7&_nc_sid=174925&_nc_ohc=Z1GTPvzRt7wAX_WbRZ5&_nc_ht=scontent.fsgn5-3.fna&oh=00_AfAYtaD2dHE_84_-PSlDqLaeyBlH9zJ3b308pHcTWucCXw&oe=642552F2", 3, "45a89e04-7a71-4399-880c-e810b791393b", null, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2001, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "namhoaidoan12@gmail.com", false, "Hieu", 0, null, new DateTime(2023, 4, 22, 15, 17, 18, 699, DateTimeKind.Local).AddTicks(7453), "Nguyen Anh", false, null, null, null, null, "AQAAAAEAACcQAAAAENVHdThIfHZVanuBQNd1mR+0uwqEhJKw65UrEbwBx5p0MHpYuuceq1f8cA8aihvPtw==", "0101010101", false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 3, "xxx", false, "namhoaidoan12@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "WorkerContructionTypes",
                columns: new[] { "BuilderId", "ConstructionTypeId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 1 },
                    { 4, 1 },
                    { 5, 1 },
                    { 6, 1 },
                    { 2, 2 },
                    { 3, 2 },
                    { 4, 2 },
                    { 5, 2 },
                    { 6, 2 },
                    { 1, 3 },
                    { 5, 3 }
                });

            migrationBuilder.InsertData(
                table: "WorkerSkills",
                columns: new[] { "WorkerSkillID", "SkillID" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 3 },
                    { 2, 4 },
                    { 3, 3 },
                    { 3, 4 },
                    { 4, 3 },
                    { 4, 4 },
                    { 5, 3 },
                    { 5, 4 },
                    { 6, 3 },
                    { 6, 4 }
                });

            migrationBuilder.InsertData(
                table: "AppliedPost",
                columns: new[] { "WorkerID", "PostID", "AppliedDate", "GroupID", "QuizId", "Status", "Video", "WishSalary" },
                values: new object[] { 4, 1, new DateTime(2023, 4, 22, 15, 17, 18, 794, DateTimeKind.Local).AddTicks(1941), null, 1, 6, null, null });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "Name", "QuizId" },
                values: new object[,]
                {
                    { 1, "Búa tạ, cáo búa  đều là những loại gì?", 1 },
                    { 2, "Quá trình phá hủy một tòa nhà được gọi là _______ ?", 1 },
                    { 3, "Khi một ngôi nhà sử dụng năng lượng mặt trời, bạn thường sẽ thấy ________ trên mái nhà.?", 1 },
                    { 4, "Tên khác của một bức tường không chịu lực là gì?", 1 }
                });

            migrationBuilder.InsertData(
                table: "Answers",
                columns: new[] { "Id", "Name", "QuestionId", "isCorrect" },
                values: new object[,]
                {
                    { 1, "Máy khoan", 1, false },
                    { 2, "Búa", 1, true },
                    { 3, "Cưa", 1, false },
                    { 4, "Vặn vít", 1, false },
                    { 5, "Phá dỡ", 2, true },
                    { 6, "Ràng buộc ", 2, false },
                    { 7, "Cốt thép", 2, false },
                    { 8, "Đùn", 2, false },
                    { 9, "Ăng-ten", 3, false },
                    { 10, "Giếng trời ", 3, false },
                    { 11, "Tấm", 3, true },
                    { 12, "Ống", 3, false },
                    { 13, "Bươm bướm", 4, false },
                    { 14, "Màn ", 4, false },
                    { 15, "Đảng", 4, true },
                    { 16, "Chỉ", 4, false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionId",
                table: "Answers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_AppliedPost_GroupID",
                table: "AppliedPost",
                column: "GroupID");

            migrationBuilder.CreateIndex(
                name: "IX_AppliedPost_QuizId",
                table: "AppliedPost",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_AppliedPost_WorkerID",
                table: "AppliedPost",
                column: "WorkerID");

            migrationBuilder.CreateIndex(
                name: "IX_Bill_ContractorId",
                table: "Bill",
                column: "ContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_Bill_StoreID",
                table: "Bill",
                column: "StoreID");

            migrationBuilder.CreateIndex(
                name: "IX_BillDetail_BillID",
                table: "BillDetail",
                column: "BillID");

            migrationBuilder.CreateIndex(
                name: "IX_BillDetail_ProductID",
                table: "BillDetail",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_BillDetail_ProductTypeId",
                table: "BillDetail",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_ProductID",
                table: "Carts",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_TypeID",
                table: "Carts",
                column: "TypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Commitment_ContractorID",
                table: "Commitment",
                column: "ContractorID");

            migrationBuilder.CreateIndex(
                name: "IX_Commitment_GroupID",
                table: "Commitment",
                column: "GroupID");

            migrationBuilder.CreateIndex(
                name: "IX_Commitment_PostID",
                table: "Commitment",
                column: "PostID");

            migrationBuilder.CreateIndex(
                name: "IX_Commitment_WorkerID",
                table: "Commitment",
                column: "WorkerID");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorPosts_ContractorID",
                table: "ContractorPosts",
                column: "ContractorID");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorPostSkills_ContractorPostID",
                table: "ContractorPostSkills",
                column: "ContractorPostID");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorPostType_ContractorPostID",
                table: "ContractorPostType",
                column: "ContractorPostID");

            migrationBuilder.CreateIndex(
                name: "IX_Group_WorkerID",
                table: "Group",
                column: "WorkerID");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMember_GroupId",
                table: "GroupMember",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMember_TypeID",
                table: "GroupMember",
                column: "TypeID");

            migrationBuilder.CreateIndex(
                name: "IX_IdentitficationCards_UserID",
                table: "IdentitficationCards",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserID",
                table: "Notifications",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_UserId",
                table: "Payments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PostInvite_ContractorId",
                table: "PostInvite",
                column: "ContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_PostInvite_ContractorPostId",
                table: "PostInvite",
                column: "ContractorPostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostInvite_WorkerID",
                table: "PostInvite",
                column: "WorkerID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_CategoriesID",
                table: "ProductCategories",
                column: "CategoriesID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_MaterialStoreID",
                table: "Products",
                column: "MaterialStoreID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypes_ColorId",
                table: "ProductTypes",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypes_OtherID",
                table: "ProductTypes",
                column: "OtherID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypes_ProductID",
                table: "ProductTypes",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypes_SizeID",
                table: "ProductTypes",
                column: "SizeID");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_QuizId",
                table: "Questions",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_Quiz_PostID",
                table: "Quiz",
                column: "PostID");

            migrationBuilder.CreateIndex(
                name: "IX_Quiz_TypeID",
                table: "Quiz",
                column: "TypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_ContractorPostId",
                table: "Reports",
                column: "ContractorPostId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_ProductId",
                table: "Reports",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_UserId",
                table: "Reports",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Saves_ContractorPostId",
                table: "Saves",
                column: "ContractorPostId");

            migrationBuilder.CreateIndex(
                name: "IX_Saves_UserId",
                table: "Saves",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_TypeId",
                table: "Skills",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswer_AnswerID",
                table: "UserAnswer",
                column: "AnswerID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ContractorId",
                table: "Users",
                column: "ContractorId",
                unique: true,
                filter: "[ContractorId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_MaterialStoreID",
                table: "Users",
                column: "MaterialStoreID",
                unique: true,
                filter: "[MaterialStoreID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_WorkerID",
                table: "Users",
                column: "WorkerID",
                unique: true,
                filter: "[WorkerID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_WorkerContructionTypes_BuilderId",
                table: "WorkerContructionTypes",
                column: "BuilderId");

            migrationBuilder.CreateIndex(
                name: "IX_Workers_TypeID",
                table: "Workers",
                column: "TypeID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkerSkills_SkillID",
                table: "WorkerSkills",
                column: "SkillID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppliedPost");

            migrationBuilder.DropTable(
                name: "BillDetail");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Commitment");

            migrationBuilder.DropTable(
                name: "ContractorPostSkills");

            migrationBuilder.DropTable(
                name: "ContractorPostType");

            migrationBuilder.DropTable(
                name: "GroupMember");

            migrationBuilder.DropTable(
                name: "IdentitficationCards");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "PostInvite");

            migrationBuilder.DropTable(
                name: "ProductCategories");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Saves");

            migrationBuilder.DropTable(
                name: "UserAnswer");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "WorkerContructionTypes");

            migrationBuilder.DropTable(
                name: "WorkerSkills");

            migrationBuilder.DropTable(
                name: "Bill");

            migrationBuilder.DropTable(
                name: "ProductTypes");

            migrationBuilder.DropTable(
                name: "Group");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "ConstructionType");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "Color");

            migrationBuilder.DropTable(
                name: "Other");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Size");

            migrationBuilder.DropTable(
                name: "Workers");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "MaterialStores");

            migrationBuilder.DropTable(
                name: "Quiz");

            migrationBuilder.DropTable(
                name: "ContractorPosts");

            migrationBuilder.DropTable(
                name: "Types");

            migrationBuilder.DropTable(
                name: "Contractors");
        }
    }
}
