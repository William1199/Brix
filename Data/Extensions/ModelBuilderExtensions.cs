
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Data.Extensions
{
    public static class ModelBuilderExtensions
    {

        public static void Seed(this ModelBuilder modelBuilder)
        {

            #region Role System
            var hasher = new Microsoft.AspNetCore.Identity.PasswordHasher<User>();
            modelBuilder.Entity<Role>().HasData(new Role
            {
                Id = Guid.Parse("52ec6e78-6732-43bf-adab-9cfa2e5da268"),
                Description = "Admin",
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
           new Role
           {
               Id = Guid.Parse("dc48ba58-ddcb-41de-96fe-e41327e5f313"),
               Description = "User",
               Name = "User",
               NormalizedName = "USER"
           },
            new Role
            {
                Id = Guid.Parse("20efd516-f16c-41b3-b11d-bc908cd2056b"),
                Description = "Contractor",
                Name = "Contractor",
                NormalizedName = "CONTRACTOR"
            },
            new Role
            {
                Id = Guid.Parse("a4fbc29e-9749-4ea0-bcaa-67fc9f104bd1"),
                Description = "Store",
                Name = "Store",
                NormalizedName = "STORE"
            });
            #endregion


            #region Construction Type
            modelBuilder.Entity<ConstructionType>().HasData(new ConstructionType
            {
                Id = 1,
                Name = "Nhà ở"
            });

            modelBuilder.Entity<ConstructionType>().HasData(new ConstructionType
            {
                Id = 2,
                Name = "Chung cư "
            });

            modelBuilder.Entity<ConstructionType>().HasData(new ConstructionType
            {
                Id = 3,
                Name = "Công trình công cộng"
            });
            #endregion

            //ADMIN
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = Guid.Parse("d7285fb7-835b-4680-a18c-673bd71f63e9"),
                Email = "admin15@gmail.com",
                UserName = "admin15@gmail.com",
                PasswordHash = hasher.HashPassword(null, "Hoainam@123"),
                SecurityStamp = string.Empty,
                FirstName = "Admin",
                LastName = "Admin",
                DOB = new DateTime(2001, 4, 30),
                PhoneNumber = "0909090909",
                Gender = Enum.Gender.MALE,
                Token = "xxx",
                Status = Enum.Status.SUCCESS,
                Avatar = "https://i1-giaitri.vnecdn.net/2013/08/15/DK-02756-1376528749.jpg?w=680&h=0&q=100&dpr=1&fit=crop&s=mX89l0q4HQgntQ5wJesOcw",
                BuilderId = 1,
                Address = "18 Tô Ký , Huyện Châu Thành , Đà Nẵng"
            });
            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = Guid.Parse("52ec6e78-6732-43bf-adab-9cfa2e5da268"),
                UserId = Guid.Parse("d7285fb7-835b-4680-a18c-673bd71f63e9"),
            });


            #region Builder 1
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = Guid.Parse("d7285fb7-835b-4680-a18c-673bd71f63d9"),
                Email = "namhoaidoan15@gmail.com",
                UserName = "namhoaidoan15@gmail.com",
                PasswordHash = hasher.HashPassword(null, "Hoainam@123"),
                SecurityStamp = string.Empty,
                FirstName = "Hoai Nam",
                LastName = "Doan Vu",
                DOB = new DateTime(2001, 4, 30),
                PhoneNumber = "0879411575",
                Gender = Enum.Gender.MALE,
                Token = "xxx",
                Status = Enum.Status.SUCCESS,
                Avatar = "https://i1-giaitri.vnecdn.net/2013/08/15/DK-02756-1376528749.jpg?w=680&h=0&q=100&dpr=1&fit=crop&s=mX89l0q4HQgntQ5wJesOcw",
                BuilderId = 1,
                Address = "41 Nguyễn Duy Trinh, Huyện Đông Hải, Bạc Liêu"
            });
            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = Guid.Parse("dc48ba58-ddcb-41de-96fe-e41327e5f313"),
                UserId = Guid.Parse("d7285fb7-835b-4680-a18c-673bd71f63d9"),
            });
            modelBuilder.Entity<Builder>().HasData(new Builder()
            {
                Id = 1,
                CreateBy = Guid.Parse("d7285fb7-835b-4680-a18c-673bd71f63d9"),
                TypeID = Guid.Parse("4ace8fcb-95eb-48c0-9deb-240e8b4e10e0"),
                Place = Enum.Place._60,
                Experience = 3,
                Certificate = "Chứng chỉ a;công ty a;09/04/2011;https://i1.rgstatic.net/publication/311457103_Certificate_of_Design_Builder_Training/links/58480cfb08aeda696825d727/largepreview.png|Chứng chỉ b;công ty b;09/04/2011;https://i1.rgstatic.net/publication/311457103_Certificate_of_Design_Builder_Training/links/58480cfb08aeda696825d727/largepreview.png",
            });

            modelBuilder.Entity<BuilderSkill>().HasData(new BuilderSkill()
            {
                BuilderSkillID = 1,
                SkillID = 1
            });

            modelBuilder.Entity<BuilderSkill>().HasData(new BuilderSkill()
            {
                BuilderSkillID = 1,
                SkillID = 2
            });

            modelBuilder.Entity<WorkerContructionType>().HasData(new WorkerContructionType
            {
                BuilderId = 1,
                ConstructionTypeId = 1
            });

            modelBuilder.Entity<WorkerContructionType>().HasData(new WorkerContructionType
            {
                BuilderId = 1,
                ConstructionTypeId = 3
            });


            #endregion

            #region Builder 2
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = Guid.Parse("d39ae0a6-9b2d-4421-be4a-cc294cec054f"),
                Email = "namhoaidoan1@gmail.com",
                UserName = "namhoaidoan1@gmail.com",
                PasswordHash = hasher.HashPassword(null, "Hoainam@123"),
                SecurityStamp = string.Empty,
                FirstName = "Thinh",
                LastName = "Nguyen Anh",
                DOB = new DateTime(2001, 9, 15),
                PhoneNumber = "0937341639",
                Gender = Enum.Gender.MALE,
                Token = "xxx",
                Status = Enum.Status.SUCCESS,
                Avatar = "https://scontent.fsgn5-3.fna.fbcdn.net/v/t1.6435-9/86186750_1329130013936346_7257030880831471616_n.jpg?_nc_cat=104&ccb=1-7&_nc_sid=174925&_nc_ohc=Z1GTPvzRt7wAX_WbRZ5&_nc_ht=scontent.fsgn5-3.fna&oh=00_AfAYtaD2dHE_84_-PSlDqLaeyBlH9zJ3b308pHcTWucCXw&oe=642552F2",
                BuilderId = 2,
                Address = "41 Nguyễn Duy Trinh, Huyện Tân Yên, Bắc Giang"
            });
            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = Guid.Parse("dc48ba58-ddcb-41de-96fe-e41327e5f313"),
                UserId = Guid.Parse("d39ae0a6-9b2d-4421-be4a-cc294cec054f"),
            });
            modelBuilder.Entity<Builder>().HasData(new Builder()
            {
                Id = 2,
                CreateBy = Guid.Parse("d39ae0a6-9b2d-4421-be4a-cc294cec054f"),
                TypeID = Guid.Parse("bd880489-5c76-4854-93ab-66e3a541bf24"),
                Experience = 3,
                Certificate = "Chứng chỉ a;công ty a;09/04/2011;https://i1.rgstatic.net/publication/311457103_Certificate_of_Design_Builder_Training/links/58480cfb08aeda696825d727/largepreview.png|Chứng chỉ b;công ty b;09/04/2011;https://i1.rgstatic.net/publication/311457103_Certificate_of_Design_Builder_Training/links/58480cfb08aeda696825d727/largepreview.png",
                Place = Enum.Place._61
            });

            modelBuilder.Entity<BuilderSkill>().HasData(new BuilderSkill()
            {
                BuilderSkillID = 2,
                SkillID = 3
            });

            modelBuilder.Entity<BuilderSkill>().HasData(new BuilderSkill()
            {
                BuilderSkillID = 2,
                SkillID = 4
            });

            modelBuilder.Entity<WorkerContructionType>().HasData(new WorkerContructionType
            {
                BuilderId = 2,
                ConstructionTypeId = 1
            });
            modelBuilder.Entity<WorkerContructionType>().HasData(new WorkerContructionType
            {
                BuilderId = 2,
                ConstructionTypeId = 2
            });
            #endregion

            #region Builder 3
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = Guid.Parse("d91f9ece-25a7-4dc6-adde-186b12c04d56"),
                Email = "namhoaidoan12@gmail.com",
                UserName = "namhoaidoan12@gmail.com",
                PasswordHash = hasher.HashPassword(null, "Hoainam@123"),
                SecurityStamp = string.Empty,
                FirstName = "Hieu",
                LastName = "Nguyen Anh",
                DOB = new DateTime(2001, 9, 15),
                PhoneNumber = "0101010101",
                Gender = Enum.Gender.MALE,
                Token = "xxx",
                Status = Enum.Status.SUCCESS,
                Avatar = "https://scontent.fsgn5-3.fna.fbcdn.net/v/t1.6435-9/86186750_1329130013936346_7257030880831471616_n.jpg?_nc_cat=104&ccb=1-7&_nc_sid=174925&_nc_ohc=Z1GTPvzRt7wAX_WbRZ5&_nc_ht=scontent.fsgn5-3.fna&oh=00_AfAYtaD2dHE_84_-PSlDqLaeyBlH9zJ3b308pHcTWucCXw&oe=642552F2",
                BuilderId = 3,
                Address = "56 Nguyễn Duy Trinh, Huyện Chợ Đồn, Bắc Kạn"
            });
            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = Guid.Parse("dc48ba58-ddcb-41de-96fe-e41327e5f313"),
                UserId = Guid.Parse("d91f9ece-25a7-4dc6-adde-186b12c04d56"),
            });
            modelBuilder.Entity<Builder>().HasData(new Builder()
            {
                Id = 3,
                CreateBy = Guid.Parse("d91f9ece-25a7-4dc6-adde-186b12c04d56"),
                TypeID = Guid.Parse("ce9fa65b-d005-46b6-953e-e6462a59cfb3"),
                Place = Enum.Place._16,
                Experience = 1,
                Certificate = "Chứng chỉ a;công ty a;09/04/2011;https://i1.rgstatic.net/publication/311457103_Certificate_of_Design_Builder_Training/links/58480cfb08aeda696825d727/largepreview.png|Chứng chỉ b;công ty b;09/04/2011;https://i1.rgstatic.net/publication/311457103_Certificate_of_Design_Builder_Training/links/58480cfb08aeda696825d727/largepreview.png",
            });

            modelBuilder.Entity<BuilderSkill>().HasData(new BuilderSkill()
            {
                BuilderSkillID = 3,
                SkillID = 3
            });

            modelBuilder.Entity<BuilderSkill>().HasData(new BuilderSkill()
            {
                BuilderSkillID = 3,
                SkillID = 4
            });

            modelBuilder.Entity<WorkerContructionType>().HasData(new WorkerContructionType
            {
                BuilderId = 3,
                ConstructionTypeId = 1
            });
            modelBuilder.Entity<WorkerContructionType>().HasData(new WorkerContructionType
            {
                BuilderId = 3,
                ConstructionTypeId = 2
            });
            #endregion

            #region Builder 4
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = Guid.Parse("319d2a06-92cc-434d-abce-7e8a33650a0d"),
                Email = "namhoaidoan13@gmail.com",
                UserName = "namhoaidoan13@gmail.com",
                PasswordHash = hasher.HashPassword(null, "Hoainam@123"),
                SecurityStamp = string.Empty,
                FirstName = "Minh",
                LastName = "Nguyen Trần",
                DOB = new DateTime(2001, 9, 15),
                PhoneNumber = "0202020202",
                Gender = Enum.Gender.MALE,
                Token = "xxx",
                Status = Enum.Status.SUCCESS,
                Avatar = "https://upload.wikimedia.org/wikipedia/commons/b/b3/%E1%BA%A2nh_ch%C3%A2n_dung_Nguy%E1%BB%85n_V%C4%83n_Minh_Tr%C3%AD.jpg",
                BuilderId = 4,
                Address = "56 Nguyễn Duy Trinh, Huyện Chợ Đồn, Bắc Kạn"
            });
            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = Guid.Parse("dc48ba58-ddcb-41de-96fe-e41327e5f313"),
                UserId = Guid.Parse("319d2a06-92cc-434d-abce-7e8a33650a0d"),
            });
            modelBuilder.Entity<Builder>().HasData(new Builder()
            {
                Id = 4,
                CreateBy = Guid.Parse("319d2a06-92cc-434d-abce-7e8a33650a0d"),
                TypeID = Guid.Parse("ce9fa65b-d005-46b6-953e-e6462a59cfb3"),
                Place = Enum.Place._52,
                Experience = 4,
                Certificate = "Chứng chỉ a;công ty a;09/04/2011;https://i1.rgstatic.net/publication/311457103_Certificate_of_Design_Builder_Training/links/58480cfb08aeda696825d727/largepreview.png|Chứng chỉ b;công ty b;09/04/2011;https://i1.rgstatic.net/publication/311457103_Certificate_of_Design_Builder_Training/links/58480cfb08aeda696825d727/largepreview.png",
            });

            modelBuilder.Entity<BuilderSkill>().HasData(new BuilderSkill()
            {
                BuilderSkillID = 4,
                SkillID = 3
            });

            modelBuilder.Entity<BuilderSkill>().HasData(new BuilderSkill()
            {
                BuilderSkillID = 4,
                SkillID = 4
            });

            modelBuilder.Entity<WorkerContructionType>().HasData(new WorkerContructionType
            {
                BuilderId = 4,
                ConstructionTypeId = 1
            });
            modelBuilder.Entity<WorkerContructionType>().HasData(new WorkerContructionType
            {
                BuilderId = 4,
                ConstructionTypeId = 2
            });
            #endregion

            #region Builder 5
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = Guid.Parse("8f314589-0c7c-40a4-b5bc-c73639664922"),
                Email = "namhoaidoan14@gmail.com",
                UserName = "namhoaidoan14@gmail.com",
                PasswordHash = hasher.HashPassword(null, "Hoainam@123"),
                SecurityStamp = string.Empty,
                FirstName = "Trúc",
                LastName = "Phạm Thanh",
                DOB = new DateTime(2001, 9, 15),
                PhoneNumber = "0303030303",
                Gender = Enum.Gender.MALE,
                Token = "xxx",
                Status = Enum.Status.SUCCESS,
                Avatar = "https://upload.wikimedia.org/wikipedia/commons/1/10/%E1%BA%A2nh-th%E1%BA%BB-v%C6%B0%E1%BB%A3ng.png",
                BuilderId = 5,
                Address = "135 Nguyễn Hiếu, Huyện Chợ Đồn, Bắc Kạn"
            });
            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = Guid.Parse("dc48ba58-ddcb-41de-96fe-e41327e5f313"),
                UserId = Guid.Parse("8f314589-0c7c-40a4-b5bc-c73639664922"),
            });
            modelBuilder.Entity<Builder>().HasData(new Builder()
            {
                Id = 5,
                CreateBy = Guid.Parse("8f314589-0c7c-40a4-b5bc-c73639664922"),
                TypeID = Guid.Parse("cf9fa65b-d005-46b6-953e-e6462a59cfb3"),
                Place = Enum.Place._42,
                Experience = 1,
                Certificate = "Chứng chỉ a;công ty a;09/04/2011;https://i1.rgstatic.net/publication/311457103_Certificate_of_Design_Builder_Training/links/58480cfb08aeda696825d727/largepreview.png|Chứng chỉ b;công ty b;09/04/2011;https://i1.rgstatic.net/publication/311457103_Certificate_of_Design_Builder_Training/links/58480cfb08aeda696825d727/largepreview.png",
            });

            modelBuilder.Entity<BuilderSkill>().HasData(new BuilderSkill()
            {
                BuilderSkillID = 5,
                SkillID = 3
            });

            modelBuilder.Entity<BuilderSkill>().HasData(new BuilderSkill()
            {
                BuilderSkillID = 5,
                SkillID = 4
            });

            modelBuilder.Entity<WorkerContructionType>().HasData(new WorkerContructionType
            {
                BuilderId = 5,
                ConstructionTypeId = 1
            });
            modelBuilder.Entity<WorkerContructionType>().HasData(new WorkerContructionType
            {
                BuilderId = 5,
                ConstructionTypeId = 2
            });
            modelBuilder.Entity<WorkerContructionType>().HasData(new WorkerContructionType
            {
                BuilderId = 5,
                ConstructionTypeId = 3
            });
            #endregion

            #region Builder 6
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = Guid.Parse("17c76dfe-7a0b-4ac9-ab8b-ba95e588a135"),
                Email = "namhoaidoan15@gmail.com",
                UserName = "namhoaidoan15@gmail.com",
                PasswordHash = hasher.HashPassword(null, "Hoainam@123"),
                SecurityStamp = string.Empty,
                FirstName = "Dương",
                LastName = "Thanh Vàng",
                DOB = new DateTime(2001, 9, 15),
                PhoneNumber = "0404040404",
                Gender = Enum.Gender.MALE,
                Token = "xxx",
                Status = Enum.Status.SUCCESS,
                Avatar = "https://demoda.vn/wp-content/uploads/2022/03/mau-anh-the-ong-chu-so-mi-trang.jpg",
                BuilderId = 6,
                Address = "135 Nguyễn Hiếu, Huyện Chợ Đồn, Bắc Kạn"
            });
            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = Guid.Parse("dc48ba58-ddcb-41de-96fe-e41327e5f313"),
                UserId = Guid.Parse("17c76dfe-7a0b-4ac9-ab8b-ba95e588a135"),
            });
            modelBuilder.Entity<Builder>().HasData(new Builder()
            {
                Id = 6,
                CreateBy = Guid.Parse("17c76dfe-7a0b-4ac9-ab8b-ba95e588a135"),
                TypeID = Guid.Parse("cf9fa65b-d005-46b6-953e-e6462a59cfb3"),
                Place = Enum.Place._13,
                Experience = 2,
                Certificate = "Chứng chỉ a;công ty a;09/04/2011;https://i1.rgstatic.net/publication/311457103_Certificate_of_Design_Builder_Training/links/58480cfb08aeda696825d727/largepreview.png|Chứng chỉ b;công ty b;09/04/2011;https://i1.rgstatic.net/publication/311457103_Certificate_of_Design_Builder_Training/links/58480cfb08aeda696825d727/largepreview.png",
            });

            modelBuilder.Entity<BuilderSkill>().HasData(new BuilderSkill()
            {
                BuilderSkillID = 6,
                SkillID = 3
            });

            modelBuilder.Entity<BuilderSkill>().HasData(new BuilderSkill()
            {
                BuilderSkillID = 6,
                SkillID = 4
            });

            modelBuilder.Entity<WorkerContructionType>().HasData(new WorkerContructionType
            {
                BuilderId = 6,
                ConstructionTypeId = 1
            });
            modelBuilder.Entity<WorkerContructionType>().HasData(new WorkerContructionType
            {
                BuilderId = 6,
                ConstructionTypeId = 2
            });

            #endregion

            #region Contractor 1
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = Guid.Parse("d7285fb7-835b-4680-a18c-673bd71f63d7"),
                Email = "contractor@gmail.com",
                PasswordHash = hasher.HashPassword(null, "Hoainam@123"),
                SecurityStamp = string.Empty,
                FirstName = "Công Ty Cổ Phần Xây Dựng Và Công Nghiệp",
                LastName = "NSN",
                UserName = "0912345678",
                DOB = new DateTime(2001, 9, 15),
                PhoneNumber = "0912345678",
                Gender = Enum.Gender.MALE,
                Token = "xxx",
                Status = Enum.Status.SUCCESS,
                Avatar = "https://www.vietnamworks.com/_next/image?url=https%3A%2F%2Fimages.vietnamworks.com%2Fpictureofcompany%2F78%2F11127264.png&w=128&q=75",
                ContractorId = 1,
                Address = "56 Nguyễn Duy Trinh, Huyện Gia Bình, Bắc Ninh"
            });
            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = Guid.Parse("20efd516-f16c-41b3-b11d-bc908cd2056b"),
                UserId = Guid.Parse("d7285fb7-835b-4680-a18c-673bd71f63d7"),
            });
            modelBuilder.Entity<Contractor>().HasData(new Contractor()
            {
                Id = 1,
                CreateBy = Guid.Parse("d7285fb7-835b-4680-a18c-673bd71f63d7"),
                CompanyName = "Công ty xây dưng Khang An",
                Description = "Hoạt động chính trong lĩnh vực: tư vấn, thiết kế, trang trí nội ngoại thất, lập dự toán công trình và xây dựng nhà ở tư nhân, nhà phố, biệt thự – vila, quán Bar – sân vườn, khách sạn, nhà hàng, showroom… Sản phẩm của chúng tôi được xây dựng theo quy trình kiểm tra chất lượng nghiêm ngặt của hệ thống quản lý chất lượng ISO 9001:2008.",
                Website = "nhaxanhqn.com",
            });
            #endregion

            #region Contractor 2
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = Guid.Parse("d7285fb7-835b-4680-a18c-673bd71f63d6"),
                Email = "contractor2@gmail.com",
                PasswordHash = hasher.HashPassword(null, "Hoainam@123"),
                SecurityStamp = string.Empty,
                FirstName = "Công Ty Cổ Phần Đầu Tư Bất Động Sản",
                LastName = "Taseco",
                UserName = "0987654321",
                DOB = new DateTime(2001, 9, 15),
                PhoneNumber = "09987654321",
                Gender = Enum.Gender.MALE,
                Token = "xxx",
                Status = Enum.Status.SUCCESS,
                Avatar = "https://www.vietnamworks.com/_next/image?url=https%3A%2F%2Fimages.vietnamworks.com%2Fpictureofcompany%2F69%2F11128477.png&w=128&q=75",
                ContractorId = 2,
                Address = "56 Nguyễn Duy Trinh, Huyện Mỏ Cày Nam, Bến Tre"
            });
            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = Guid.Parse("20efd516-f16c-41b3-b11d-bc908cd2056b"),
                UserId = Guid.Parse("d7285fb7-835b-4680-a18c-673bd71f63d6"),
            });
            modelBuilder.Entity<Contractor>().HasData(new Contractor()
            {
                Id = 2,
                CreateBy = Guid.Parse("d7285fb7-835b-4680-a18c-673bd71f63d6"),
                CompanyName = "Công ty xây dưng Đất Xanh",
                Description = "Trong những năm vừa qua được sự ưu ái và tín nhiệm của Quý khách hàng Công ty Đất Xanh từng bước trưởng thành và trở thành đơn vị hoạt động trong lĩnh vực tư vấn, thiết kế và xây dựng dân dụng hàng đầu tại Việt Nam.",
                Website = "xaydunglaco.vn",

            });
            #endregion

            #region Contractor 3
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = Guid.Parse("86b8070e-00c5-45de-8db7-199cee7350d9"),
                Email = "contractor3@gmail.com",
                PasswordHash = hasher.HashPassword(null, "Hoainam@123"),
                SecurityStamp = string.Empty,
                FirstName = "Công ty TNHH ",
                LastName = "Ánh Nhiên Xanh",
                UserName = "0987654321",
                DOB = new DateTime(2001, 9, 15),
                PhoneNumber = "0888694499",
                Gender = Enum.Gender.MALE,
                Token = "xxx",
                Status = Enum.Status.SUCCESS,
                Avatar = "https://diaocthinhvuong.vn/wp-content/uploads/2021/05/1logo-newtecons.jpg",
                ContractorId = 3,
                Address = "56 Nguyễn Duy Trinh, Huyện Mỏ Cày Nam, Bến Tre"
            });
            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = Guid.Parse("20efd516-f16c-41b3-b11d-bc908cd2056b"),
                UserId = Guid.Parse("86b8070e-00c5-45de-8db7-199cee7350d9"),
            });
            modelBuilder.Entity<Contractor>().HasData(new Contractor()
            {
                Id = 3,
                CreateBy = Guid.Parse("86b8070e-00c5-45de-8db7-199cee7350d9"),
                CompanyName = "Công ty xây dưng Ánh Nhiên Xanh",
                Description = "Trong những năm vừa qua được sự ưu ái và tín nhiệm của Quý khách hàng Công ty Ánh Nhiên Xanh từng bước trưởng thành và trở thành đơn vị hoạt động trong lĩnh vực tư vấn, thiết kế và xây dựng dân dụng hàng đầu tại Việt Nam.",
                Website = "xaydunganhnhien.vn",

            });
            #endregion

            #region Store 1
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = Guid.Parse("b57b172a-a044-11ed-a8fc-0242ac120002"),
                Email = "store123@gmail.com",
                PasswordHash = hasher.HashPassword(null, "Hoainam@123"),
                SecurityStamp = string.Empty,
                FirstName = "TPHCM",
                LastName = "Cửa Hàng Vật Liệu",
                UserName = "0924516734",
                DOB = new DateTime(2001, 9, 15),
                PhoneNumber = "0924516734",
                Gender = Enum.Gender.MALE,
                Token = "xxx",
                Status = Enum.Status.SUCCESS,
                Avatar = "https://baodautu.vn/Images/chicong/2018/11/28/thi-truong-vat-lieu-xay-dung-mua-kinh-doanh-da-thay-doi1543390455.jpg",
                MaterialStoreID = 1,
                Address = "56 Nguyễn Duy Trinh, Huyện An Lão, Bình Định"
            });
            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = Guid.Parse("a4fbc29e-9749-4ea0-bcaa-67fc9f104bd1"),
                UserId = Guid.Parse("b57b172a-a044-11ed-a8fc-0242ac120002"),
            });
            modelBuilder.Entity<MaterialStore>().HasData(new MaterialStore()
            {
                Id = 1,
                Website = "https://vinasoftware.com.vn/",
                Description = "Với mục tiêu cung cấp nhiều gói sản phẩm phong phú về mẫu mã và các tính năng linh hoạt cho nhiều loại hình website như giới thiệu công ty, bán hàng, trang tin tức, thương mại điện tử… cùng với nhiều giao diện phong phú đa dạng độc đáo đã được VNS lọc chọn và đúc kết nhằm giới thiệu tới khách hàng với mong muốn có một website nhanh, đẹp, hiệu quả và giá cả hợp lý.",
                CreateBy = Guid.Parse("b57b172a-a044-11ed-a8fc-0242ac120002"),
                Experience = "Hiện đang là đại lý cấp 1 phân phối các sản phẩm chất lượng, có thương hiệu nổi tiếng, giá thành phù hợp với giá niêm yết của nhà máy. Công ty còn cung cấp và phân phối nhiều loại cát bê tông và cát xây dựng được sàng và rửa tại dây chuyền sản xuất. Ngoài ra còn phân phối nhiều loại vật liệu xây dựng khác như xi măng, gạch, sắt thép đảm bảo chất lượng cao để sử dụng cho các công trình xây dựng.",
                Place = Enum.Place._61,
                TaxCode = "8156184163",
            });
            #endregion

            #region Store 2
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = Guid.Parse("be21b564-a044-11ed-a8fc-0242ac120002"),
                Email = "store2@gmail.com",
                PasswordHash = hasher.HashPassword(null, "Hoainam@123"),
                SecurityStamp = string.Empty,
                FirstName = "Cần Thơ",
                LastName = "VLXD",
                UserName = "09245167342",
                DOB = new DateTime(2001, 9, 15),
                PhoneNumber = "09245167342",
                Gender = Enum.Gender.MALE,
                Token = "xxx",
                Status = Enum.Status.SUCCESS,
                Avatar = "https://thaicong.com/wp-content/uploads/2017/11/img_sub_4.jpg",
                MaterialStoreID = 2,
                Address = "56 Nguyễn Duy Trinh, Huyện Hàm Tân, Bình Thuận"
            });
            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = Guid.Parse("a4fbc29e-9749-4ea0-bcaa-67fc9f104bd1"),
                UserId = Guid.Parse("be21b564-a044-11ed-a8fc-0242ac120002"),
            });
            modelBuilder.Entity<MaterialStore>().HasData(new MaterialStore()
            {
                Id = 2,
                CreateBy = Guid.Parse("be21b564-a044-11ed-a8fc-0242ac120002"),
                Description = "Về vật liệu xây dựng, công ty luôn có sẵn hàng hóa để khách hàng so sánh và lựa chọn, ngoài ra còn có đội xe lớn nhỏ sẵn sàng giao hàng trong thời gian sớm nhất.",
                Experience = "Hiện đang là đại lý cấp 1 phân phối các sản phẩm chất lượng, có thương hiệu nổi tiếng, giá thành phù hợp với giá niêm yết của nhà máy. Công ty còn cung cấp và phân phối nhiều loại cát bê tông và cát xây dựng được sàng và rửa tại dây chuyền sản xuất. Ngoài ra còn phân phối nhiều loại vật liệu xây dựng khác như xi măng, gạch, sắt thép đảm bảo chất lượng cao để sử dụng cho các công trình xây dựng.",
                Place = Enum.Place._61,
                TaxCode = "8156284563",
            });
            #endregion

            #region Builder Type

            modelBuilder.Entity<Entities.Type>().HasData(new Entities.Type
            {
                Id = Guid.Parse("4ace8fcb-95eb-48c0-9deb-240e8b4e10e0"),
                Name = "Thợ xây"
            });
            modelBuilder.Entity<Entities.Type>().HasData(new Entities.Type
            {
                Id = Guid.Parse("bd880489-5c76-4854-93ab-66e3a541bf24"),
                Name = "Thợ hồ"
            });
            modelBuilder.Entity<Entities.Type>().HasData(new Entities.Type
            {
                Id = Guid.Parse("ce9fa65b-d005-46b6-953e-e6462a59cfb3"),
                Name = "Thợ sơn"
            });
            modelBuilder.Entity<Entities.Type>().HasData(new Entities.Type
            {
                Id = Guid.Parse("cf9fa65b-d005-46b6-953e-e6462a59cfb3"),
                Name = "Thợ hàn"
            });
            #endregion

            #region Skills

            modelBuilder.Entity<Entities.Skill>().HasData(new Entities.Skill
            {
                Id = 1,
                Name = "Xây dựng",
                FromSystem = true,
                TypeId = Guid.Parse("4ace8fcb-95eb-48c0-9deb-240e8b4e10e0")
            });
            modelBuilder.Entity<Entities.Skill>().HasData(new Entities.Skill
            {
                Id = 2,
                Name = "Sơn tường",
                FromSystem = true,
                TypeId = Guid.Parse("ce9fa65b-d005-46b6-953e-e6462a59cfb3")
            });
            modelBuilder.Entity<Entities.Skill>().HasData(new Entities.Skill
            {
                Id = 3,
                Name = "Lát gạch",
                FromSystem = true,
                TypeId = Guid.Parse("bd880489-5c76-4854-93ab-66e3a541bf24")
            });
            modelBuilder.Entity<Entities.Skill>().HasData(new Entities.Skill
            {
                Id = 4,
                Name = "Thạch Cao",
                FromSystem = true,
                TypeId = Guid.Parse("ce9fa65b-d005-46b6-953e-e6462a59cfb3")
            });

            modelBuilder.Entity<Entities.Skill>().HasData(new Entities.Skill
            {
                Id = 5,
                Name = "Điện",
                FromSystem = true,
                TypeId = Guid.Parse("ce9fa65b-d005-46b6-953e-e6462a59cfb3")
            });

            modelBuilder.Entity<Entities.Skill>().HasData(new Entities.Skill
            {
                Id = 6,
                Name = "Nước",
                FromSystem = true,
                TypeId = Guid.Parse("ce9fa65b-d005-46b6-953e-e6462a59cfb3")
            });

            modelBuilder.Entity<Entities.Skill>().HasData(new Entities.Skill
            {
                Id = 7,
                Name = "Hàn Sắt",
                FromSystem = true,
                TypeId = Guid.Parse("ce9fa65b-d005-46b6-953e-e6462a59cfb3")
            });

            modelBuilder.Entity<Entities.Skill>().HasData(new Entities.Skill
            {
                Id = 8,
                Name = "Ốp lát",
                FromSystem = true,
                TypeId = Guid.Parse("ce9fa65b-d005-46b6-953e-e6462a59cfb3")
            });

            #endregion

            #region Contractor Post 1

            modelBuilder.Entity<ContractorPost>().HasData(new ContractorPost
            {
                Id = 1,
                Title = "Tuyển dụng thợ xây lành nghề CẦN GẤP",
                ProjectName = "TPHCM - QUẬN 7 - TUYỂN DỤNG GẤP",
                Description = "<div>I. THÔNG TIN CHUNG</div><div>1. Quy trình công việc liên quan: Quy trình quản lí dự án (mảng bản vẽ, báo giá, tiến độ)</div><div>2. Cấp trực tiếp quản lý: Giám đốc dự án</div><div>3. Loại hợp đồng: Hợp đồng xác định có thời hạn/không thời hạn</div><div><br></div><div>II. MỤC ĐÍCH CÔNG VIỆC</div><div>Nắm bản vẽ của dự án từ lúc đấu thầu, hiểu rõ các spect của dự án để báo giá. Có khả năng bốc khối lượng để đối ứng với cá báo giá gấp, các hạng mục phát sinh. Khi dự án trúng thấu, có khả năng điều phối dự án ở vai trò quản lí thiết kế, quản lí tiến độ, hoặc quản lí chất lượng (đối với dự án quy mô nhỏ)</div><div><br></div><div>III. TRÁCH NHIỆM VÀ NHIỆM VỤ</div><div>1. Làm báo giá dự án Nhật và hỗ trợ giám đốc dự án đi đấu thầu</div><div>• Nắm rõ bản vẽ của dự án từ lúc đấu thầu</div><div>• Hiểu rõ các vật tư spect của ngành kết cấu thép để báo giá</div><div>• Có khả năng bốc khối lượng cho các dự án gấp</div><div>• Có khả năng lên các bản vẽ đề xuất bằng CAD</div><div>• Cùng với GDDA đi đấu thầu các dự án</div><div>• Tiếp khách, đối ứng khách khi có audit, khách về việt nam</div><div><br></div><div>2. Quản lí dự án trúng thầu mảng quản lí bản vẽ thiết kế, quản lí tiến độ</div><div>• Nắm rõ các thay đổi thiết kế, chỉ thị bản vẽ của khách để triển khai cho shop</div><div>• Sử dụng thành thạo Tekla hoặc phần mềm real 4 để xuất các giấy tờ phục vụ cho quản lí dự án</div><div>• Lên được kế hoạch sản xuất và quản lí sản xuất, xuất hàng</div><div><br></div><div>3. Đối ứng khách hàng</div><div>• Báo cáo tiến độ sản xuất, bản vẽ</div><div>• Tham gia chủ đạo trong các cuộc họp tiến độ với nhà máy</div><div><br></div><div>4 . Nghiên cứu &amp; Phát triển</div><div>• Tham gia nghiên cứu và phát triển giải pháp công nghệ sản xuất</div><div><br></div><div>5. Tổng kết và đúc kết kinh nghiệm quản lí dự án</div><div>• Tổng kết thường xuyên các vướng mắt trong tiến độ, bản vẽ . Các lỗi hay mắc phải, hướng xử lí</div><div>• Đối với NCR đóng vai trò chỉ huy xử lí (liên quan tiến độ, chất lượng)</div><div><br></div><div>6. Cost control</div><div>• Phối hợp với giám đốc dự án trong công tác giám sát ngân sách thực hiện</div><div><br></div><div>7. Nhiệm vụ khác</div><div>• Các nhiệm vụ khác được phân công</div>",
                Benefit = "<div>- Tăng lương 6 tháng một lần. Thưởng vào các ngày lễ tết. Cơ hội thăng tiến cao.</div><div>- Xe đưa rước hàng ngày</div><div>- Khám sức khỏe &amp; đi du lịch và các chế độ khác theo quy định của pháp luật Lao động</div>",
                Required = "<div>1. Trình độ đào tạo:</div><div>• Bằng cấp tối thiểu: Cao đẳng, Đại học</div><div>• Chuyên ngành đào tạo: Xây dựng Dân dụng và Công nghiệp, Cơ khí</div><div>• Chứng chỉ, giấy phép hành nghề (nếu có): không yêu cầu.Ưu tiên nếu có chứng chỉ:….</div><div>• Trình độ Ngoại ngữ (tiếng anh, tiếng nhật, tiếng trung): Tiếng Nhật (bắt buộc, N2 đổ lên )</div><div><br></div><div>2. Kiến thức cần có:</div><div>• Sản phẩm dịch vụ của Công ty</div><div>• Kiến thức về lĩnh vực kết cấu thép (tiêu chuẩn, vật liệu, sản xuất, quy trình thi công, lắp dựng)</div><div><br></div><div>3. Năng lực cần thiết:</div><div>Năng lực quản lý</div><div>•Nếu có kinh nghiệm làm ở Nhật, hoặc ở công ty Nhật lâu năm càng tốt</div><div>Năng lực chuyên môn</div><div>•Có bằng cấp liên quan đến kết cấu thép Nhật càng tốt</div><div>•Kỹ năng vẽ CAD cơ bản, Sử dụng thành thạo Tekla hoặc phần mềm real 4</div><div>•Đọc hiểu bản vẽ lắp dựng, bản vẽ tổng thể, tiêu chuẩn đường hàn, tiêu chuẩn JASS 6, ASTM , …</div><div><br></div><div>Năng lực bổ trợ</div><div>• Kỹ năng giao tiếp</div><div>• Kỹ năng giải quyết vấn đề và ra quyết định</div><div>• Kỹ năng làm việc nhóm</div><div>• Kỹ năng sử dụng thành thạo tin học văn phòng</div><div><br></div><div>4. Thái độ hoặc tố chất</div><div>• Có tinh thần trách nhiệm</div><div>• Cẩn thận, tỉ mỉ, chủ động, cầu tiến</div><div><br></div><div>5. Yêu cầu khác:</div><div>•Có đủ sức khỏe để đáp ứng yêu cầu nhiệm vụ được giao</div><div>•Có thể đi công tác</div>",
                StarDate = new DateTime(2023, 3, 15),
                EndDate = new DateTime(2023, 5, 15),
                Status = Enum.Status.SUCCESS,
                Place = Enum.Place._52,
                PostCategories = Enum.PostCategories.CO_DIEN,
                Salaries = "200000 - 800000",
                NumberPeople = 20,
                ContractorID = 1,
                Accommodation = true,
                ConstructionType = "Nhà ở",
                Transport = true,
                StartTime = "8:00",
                EndTime = "17:30",
                CreateBy = Guid.Parse("d7285fb7-835b-4680-a18c-673bd71f63d7"),
                QuizRequired = true
            });

            modelBuilder.Entity<ContractorPostType>().HasData(new ContractorPostType
            {
                ContractorPostID = 1,
                TypeID = Guid.Parse("4ace8fcb-95eb-48c0-9deb-240e8b4e10e0")
            });

            modelBuilder.Entity<ContractorPostType>().HasData(new ContractorPostType
            {
                ContractorPostID = 1,
                TypeID = Guid.Parse("ce9fa65b-d005-46b6-953e-e6462a59cfb3")
            });


            modelBuilder.Entity<ContractorPostSkill>().HasData(new ContractorPostSkill
            {
                ContractorPostID = 1,
                SkillID = 1
            });

            modelBuilder.Entity<ContractorPostSkill>().HasData(new ContractorPostSkill
            {
                ContractorPostID = 1,
                SkillID = 2
            });




            #endregion

            #region Contractor Post 2
            modelBuilder.Entity<ContractorPost>().HasData(new ContractorPost
            {
                Id = 2,
                Title = "(CẦN GẤP) Tuyển thợ xây ",
                ProjectName = "TPHCM - QUẬN 7 - TUYỂN DỤNG GẤP",
                Description = "<div>I. THÔNG TIN CHUNG</div><div>1. Quy trình công việc liên quan: Quy trình quản lí dự án (mảng bản vẽ, báo giá, tiến độ)</div><div>2. Cấp trực tiếp quản lý: Giám đốc dự án</div><div>3. Loại hợp đồng: Hợp đồng xác định có thời hạn/không thời hạn</div><div><br></div><div>II. MỤC ĐÍCH CÔNG VIỆC</div><div>Nắm bản vẽ của dự án từ lúc đấu thầu, hiểu rõ các spect của dự án để báo giá. Có khả năng bốc khối lượng để đối ứng với cá báo giá gấp, các hạng mục phát sinh. Khi dự án trúng thấu, có khả năng điều phối dự án ở vai trò quản lí thiết kế, quản lí tiến độ, hoặc quản lí chất lượng (đối với dự án quy mô nhỏ)</div><div><br></div><div>III. TRÁCH NHIỆM VÀ NHIỆM VỤ</div><div>1. Làm báo giá dự án Nhật và hỗ trợ giám đốc dự án đi đấu thầu</div><div>• Nắm rõ bản vẽ của dự án từ lúc đấu thầu</div><div>• Hiểu rõ các vật tư spect của ngành kết cấu thép để báo giá</div><div>• Có khả năng bốc khối lượng cho các dự án gấp</div><div>• Có khả năng lên các bản vẽ đề xuất bằng CAD</div><div>• Cùng với GDDA đi đấu thầu các dự án</div><div>• Tiếp khách, đối ứng khách khi có audit, khách về việt nam</div><div><br></div><div>2. Quản lí dự án trúng thầu mảng quản lí bản vẽ thiết kế, quản lí tiến độ</div><div>• Nắm rõ các thay đổi thiết kế, chỉ thị bản vẽ của khách để triển khai cho shop</div><div>• Sử dụng thành thạo Tekla hoặc phần mềm real 4 để xuất các giấy tờ phục vụ cho quản lí dự án</div><div>• Lên được kế hoạch sản xuất và quản lí sản xuất, xuất hàng</div><div><br></div><div>3. Đối ứng khách hàng</div><div>• Báo cáo tiến độ sản xuất, bản vẽ</div><div>• Tham gia chủ đạo trong các cuộc họp tiến độ với nhà máy</div><div><br></div><div>4 . Nghiên cứu &amp; Phát triển</div><div>• Tham gia nghiên cứu và phát triển giải pháp công nghệ sản xuất</div><div><br></div><div>5. Tổng kết và đúc kết kinh nghiệm quản lí dự án</div><div>• Tổng kết thường xuyên các vướng mắt trong tiến độ, bản vẽ . Các lỗi hay mắc phải, hướng xử lí</div><div>• Đối với NCR đóng vai trò chỉ huy xử lí (liên quan tiến độ, chất lượng)</div><div><br></div><div>6. Cost control</div><div>• Phối hợp với giám đốc dự án trong công tác giám sát ngân sách thực hiện</div><div><br></div><div>7. Nhiệm vụ khác</div><div>• Các nhiệm vụ khác được phân công</div>",
                Benefit = "<div>- Tăng lương 6 tháng một lần. Thưởng vào các ngày lễ tết. Cơ hội thăng tiến cao.</div><div>- Xe đưa rước hàng ngày</div><div>- Khám sức khỏe &amp; đi du lịch và các chế độ khác theo quy định của pháp luật Lao động</div>",
                Required = "<div>1. Trình độ đào tạo:</div><div>• Bằng cấp tối thiểu: Cao đẳng, Đại học</div><div>• Chuyên ngành đào tạo: Xây dựng Dân dụng và Công nghiệp, Cơ khí</div><div>• Chứng chỉ, giấy phép hành nghề (nếu có): không yêu cầu.Ưu tiên nếu có chứng chỉ:….</div><div>• Trình độ Ngoại ngữ (tiếng anh, tiếng nhật, tiếng trung): Tiếng Nhật (bắt buộc, N2 đổ lên )</div><div><br></div><div>2. Kiến thức cần có:</div><div>• Sản phẩm dịch vụ của Công ty</div><div>• Kiến thức về lĩnh vực kết cấu thép (tiêu chuẩn, vật liệu, sản xuất, quy trình thi công, lắp dựng)</div><div><br></div><div>3. Năng lực cần thiết:</div><div>Năng lực quản lý</div><div>•Nếu có kinh nghiệm làm ở Nhật, hoặc ở công ty Nhật lâu năm càng tốt</div><div>Năng lực chuyên môn</div><div>•Có bằng cấp liên quan đến kết cấu thép Nhật càng tốt</div><div>•Kỹ năng vẽ CAD cơ bản, Sử dụng thành thạo Tekla hoặc phần mềm real 4</div><div>•Đọc hiểu bản vẽ lắp dựng, bản vẽ tổng thể, tiêu chuẩn đường hàn, tiêu chuẩn JASS 6, ASTM , …</div><div><br></div><div>Năng lực bổ trợ</div><div>• Kỹ năng giao tiếp</div><div>• Kỹ năng giải quyết vấn đề và ra quyết định</div><div>• Kỹ năng làm việc nhóm</div><div>• Kỹ năng sử dụng thành thạo tin học văn phòng</div><div><br></div><div>4. Thái độ hoặc tố chất</div><div>• Có tinh thần trách nhiệm</div><div>• Cẩn thận, tỉ mỉ, chủ động, cầu tiến</div><div><br></div><div>5. Yêu cầu khác:</div><div>•Có đủ sức khỏe để đáp ứng yêu cầu nhiệm vụ được giao</div><div>•Có thể đi công tác</div>",
                StarDate = new DateTime(2023, 3, 15),
                EndDate = new DateTime(2023, 5, 15),
                Status = Enum.Status.PENDING,
                Place = Enum.Place._52,
                PostCategories = Enum.PostCategories.CO_DIEN,
                Salaries = "200000 - 500000",
                NumberPeople = 20,
                Accommodation = true,
                ConstructionType = "Tòa nhà/Chung cư",
                Transport = true,
                StartTime = "8:00",
                EndTime = "17:30",
                ContractorID = 1,
                CreateBy = Guid.Parse("d7285fb7-835b-4680-a18c-673bd71f63d7"),
                QuizRequired = true,

            });

            modelBuilder.Entity<ContractorPostType>().HasData(new ContractorPostType
            {
                ContractorPostID = 2,
                TypeID = Guid.Parse("4ace8fcb-95eb-48c0-9deb-240e8b4e10e0")
            });


            modelBuilder.Entity<ContractorPostSkill>().HasData(new ContractorPostSkill
            {
                ContractorPostID = 2,
                SkillID = 1
            });

            modelBuilder.Entity<ContractorPostSkill>().HasData(new ContractorPostSkill
            {
                ContractorPostID = 2,
                SkillID = 3
            });
            modelBuilder.Entity<ContractorPostSkill>().HasData(new ContractorPostSkill
            {
                ContractorPostID = 2,
                SkillID = 5
            });
            #endregion

            #region Contractor Post 3
            modelBuilder.Entity<ContractorPost>().HasData(new ContractorPost
            {
                Id = 3,
                Title = "Tuyển Công nhân xây dựng lành nghê ",
                ProjectName = "TPHCM - QUẬN 7 - TUYỂN DỤNG GẤP",
                Description = "<div>I. THÔNG TIN CHUNG</div><div>1. Quy trình công việc liên quan: Quy trình quản lí dự án (mảng bản vẽ, báo giá, tiến độ)</div><div>2. Cấp trực tiếp quản lý: Giám đốc dự án</div><div>3. Loại hợp đồng: Hợp đồng xác định có thời hạn/không thời hạn</div><div><br></div><div>II. MỤC ĐÍCH CÔNG VIỆC</div><div>Nắm bản vẽ của dự án từ lúc đấu thầu, hiểu rõ các spect của dự án để báo giá. Có khả năng bốc khối lượng để đối ứng với cá báo giá gấp, các hạng mục phát sinh. Khi dự án trúng thấu, có khả năng điều phối dự án ở vai trò quản lí thiết kế, quản lí tiến độ, hoặc quản lí chất lượng (đối với dự án quy mô nhỏ)</div><div><br></div><div>III. TRÁCH NHIỆM VÀ NHIỆM VỤ</div><div>1. Làm báo giá dự án Nhật và hỗ trợ giám đốc dự án đi đấu thầu</div><div>• Nắm rõ bản vẽ của dự án từ lúc đấu thầu</div><div>• Hiểu rõ các vật tư spect của ngành kết cấu thép để báo giá</div><div>• Có khả năng bốc khối lượng cho các dự án gấp</div><div>• Có khả năng lên các bản vẽ đề xuất bằng CAD</div><div>• Cùng với GDDA đi đấu thầu các dự án</div><div>• Tiếp khách, đối ứng khách khi có audit, khách về việt nam</div><div><br></div><div>2. Quản lí dự án trúng thầu mảng quản lí bản vẽ thiết kế, quản lí tiến độ</div><div>• Nắm rõ các thay đổi thiết kế, chỉ thị bản vẽ của khách để triển khai cho shop</div><div>• Sử dụng thành thạo Tekla hoặc phần mềm real 4 để xuất các giấy tờ phục vụ cho quản lí dự án</div><div>• Lên được kế hoạch sản xuất và quản lí sản xuất, xuất hàng</div><div><br></div><div>3. Đối ứng khách hàng</div><div>• Báo cáo tiến độ sản xuất, bản vẽ</div><div>• Tham gia chủ đạo trong các cuộc họp tiến độ với nhà máy</div><div><br></div><div>4 . Nghiên cứu &amp; Phát triển</div><div>• Tham gia nghiên cứu và phát triển giải pháp công nghệ sản xuất</div><div><br></div><div>5. Tổng kết và đúc kết kinh nghiệm quản lí dự án</div><div>• Tổng kết thường xuyên các vướng mắt trong tiến độ, bản vẽ . Các lỗi hay mắc phải, hướng xử lí</div><div>• Đối với NCR đóng vai trò chỉ huy xử lí (liên quan tiến độ, chất lượng)</div><div><br></div><div>6. Cost control</div><div>• Phối hợp với giám đốc dự án trong công tác giám sát ngân sách thực hiện</div><div><br></div><div>7. Nhiệm vụ khác</div><div>• Các nhiệm vụ khác được phân công</div>",
                Benefit = "<div>- Tăng lương 6 tháng một lần. Thưởng vào các ngày lễ tết. Cơ hội thăng tiến cao.</div><div>- Xe đưa rước hàng ngày</div><div>- Khám sức khỏe &amp; đi du lịch và các chế độ khác theo quy định của pháp luật Lao động</div>",
                Required = "<div>1. Trình độ đào tạo:</div><div>• Bằng cấp tối thiểu: Cao đẳng, Đại học</div><div>• Chuyên ngành đào tạo: Xây dựng Dân dụng và Công nghiệp, Cơ khí</div><div>• Chứng chỉ, giấy phép hành nghề (nếu có): không yêu cầu.Ưu tiên nếu có chứng chỉ:….</div><div>• Trình độ Ngoại ngữ (tiếng anh, tiếng nhật, tiếng trung): Tiếng Nhật (bắt buộc, N2 đổ lên )</div><div><br></div><div>2. Kiến thức cần có:</div><div>• Sản phẩm dịch vụ của Công ty</div><div>• Kiến thức về lĩnh vực kết cấu thép (tiêu chuẩn, vật liệu, sản xuất, quy trình thi công, lắp dựng)</div><div><br></div><div>3. Năng lực cần thiết:</div><div>Năng lực quản lý</div><div>•Nếu có kinh nghiệm làm ở Nhật, hoặc ở công ty Nhật lâu năm càng tốt</div><div>Năng lực chuyên môn</div><div>•Có bằng cấp liên quan đến kết cấu thép Nhật càng tốt</div><div>•Kỹ năng vẽ CAD cơ bản, Sử dụng thành thạo Tekla hoặc phần mềm real 4</div><div>•Đọc hiểu bản vẽ lắp dựng, bản vẽ tổng thể, tiêu chuẩn đường hàn, tiêu chuẩn JASS 6, ASTM , …</div><div><br></div><div>Năng lực bổ trợ</div><div>• Kỹ năng giao tiếp</div><div>• Kỹ năng giải quyết vấn đề và ra quyết định</div><div>• Kỹ năng làm việc nhóm</div><div>• Kỹ năng sử dụng thành thạo tin học văn phòng</div><div><br></div><div>4. Thái độ hoặc tố chất</div><div>• Có tinh thần trách nhiệm</div><div>• Cẩn thận, tỉ mỉ, chủ động, cầu tiến</div><div><br></div><div>5. Yêu cầu khác:</div><div>•Có đủ sức khỏe để đáp ứng yêu cầu nhiệm vụ được giao</div><div>•Có thể đi công tác</div>",
                StarDate = new DateTime(2023, 3, 15),
                EndDate = new DateTime(2023, 5, 15),
                Status = Enum.Status.SUCCESS,
                Place = Enum.Place._52,
                PostCategories = Enum.PostCategories.CO_DIEN,
                Salaries = "+600000",
                NumberPeople = 20,
                ContractorID = 2,
                Accommodation = true,
                ConstructionType = "Công trình công cộng",
                Transport = true,
                StartTime = "8:00",
                EndTime = "17:30",
                CreateBy = Guid.Parse("d7285fb7-835b-4680-a18c-673bd71f63d6")

            });

            modelBuilder.Entity<ContractorPostSkill>().HasData(new ContractorPostSkill
            {
                ContractorPostID = 3,
                SkillID = 3
            });


            #endregion

            #region Contractor Post 4
            modelBuilder.Entity<ContractorPost>().HasData(new ContractorPost
            {
                Id = 4,
                Title = "Cần người làm dự án (GẤP)",
                ProjectName = "TPHCM - QUẬN 7 - TUYỂN DỤNG GẤP",
                Description = "<div>I. THÔNG TIN CHUNG</div><div>1. Quy trình công việc liên quan: Quy trình quản lí dự án (mảng bản vẽ, báo giá, tiến độ)</div><div>2. Cấp trực tiếp quản lý: Giám đốc dự án</div><div>3. Loại hợp đồng: Hợp đồng xác định có thời hạn/không thời hạn</div><div><br></div><div>II. MỤC ĐÍCH CÔNG VIỆC</div><div>Nắm bản vẽ của dự án từ lúc đấu thầu, hiểu rõ các spect của dự án để báo giá. Có khả năng bốc khối lượng để đối ứng với cá báo giá gấp, các hạng mục phát sinh. Khi dự án trúng thấu, có khả năng điều phối dự án ở vai trò quản lí thiết kế, quản lí tiến độ, hoặc quản lí chất lượng (đối với dự án quy mô nhỏ)</div><div><br></div><div>III. TRÁCH NHIỆM VÀ NHIỆM VỤ</div><div>1. Làm báo giá dự án Nhật và hỗ trợ giám đốc dự án đi đấu thầu</div><div>• Nắm rõ bản vẽ của dự án từ lúc đấu thầu</div><div>• Hiểu rõ các vật tư spect của ngành kết cấu thép để báo giá</div><div>• Có khả năng bốc khối lượng cho các dự án gấp</div><div>• Có khả năng lên các bản vẽ đề xuất bằng CAD</div><div>• Cùng với GDDA đi đấu thầu các dự án</div><div>• Tiếp khách, đối ứng khách khi có audit, khách về việt nam</div><div><br></div><div>2. Quản lí dự án trúng thầu mảng quản lí bản vẽ thiết kế, quản lí tiến độ</div><div>• Nắm rõ các thay đổi thiết kế, chỉ thị bản vẽ của khách để triển khai cho shop</div><div>• Sử dụng thành thạo Tekla hoặc phần mềm real 4 để xuất các giấy tờ phục vụ cho quản lí dự án</div><div>• Lên được kế hoạch sản xuất và quản lí sản xuất, xuất hàng</div><div><br></div><div>3. Đối ứng khách hàng</div><div>• Báo cáo tiến độ sản xuất, bản vẽ</div><div>• Tham gia chủ đạo trong các cuộc họp tiến độ với nhà máy</div><div><br></div><div>4 . Nghiên cứu &amp; Phát triển</div><div>• Tham gia nghiên cứu và phát triển giải pháp công nghệ sản xuất</div><div><br></div><div>5. Tổng kết và đúc kết kinh nghiệm quản lí dự án</div><div>• Tổng kết thường xuyên các vướng mắt trong tiến độ, bản vẽ . Các lỗi hay mắc phải, hướng xử lí</div><div>• Đối với NCR đóng vai trò chỉ huy xử lí (liên quan tiến độ, chất lượng)</div><div><br></div><div>6. Cost control</div><div>• Phối hợp với giám đốc dự án trong công tác giám sát ngân sách thực hiện</div><div><br></div><div>7. Nhiệm vụ khác</div><div>• Các nhiệm vụ khác được phân công</div>",
                Benefit = "<div>- Tăng lương 6 tháng một lần. Thưởng vào các ngày lễ tết. Cơ hội thăng tiến cao.</div><div>- Xe đưa rước hàng ngày</div><div>- Khám sức khỏe &amp; đi du lịch và các chế độ khác theo quy định của pháp luật Lao động</div>",
                Required = "<div>1. Trình độ đào tạo:</div><div>• Bằng cấp tối thiểu: Cao đẳng, Đại học</div><div>• Chuyên ngành đào tạo: Xây dựng Dân dụng và Công nghiệp, Cơ khí</div><div>• Chứng chỉ, giấy phép hành nghề (nếu có): không yêu cầu.Ưu tiên nếu có chứng chỉ:….</div><div>• Trình độ Ngoại ngữ (tiếng anh, tiếng nhật, tiếng trung): Tiếng Nhật (bắt buộc, N2 đổ lên )</div><div><br></div><div>2. Kiến thức cần có:</div><div>• Sản phẩm dịch vụ của Công ty</div><div>• Kiến thức về lĩnh vực kết cấu thép (tiêu chuẩn, vật liệu, sản xuất, quy trình thi công, lắp dựng)</div><div><br></div><div>3. Năng lực cần thiết:</div><div>Năng lực quản lý</div><div>•Nếu có kinh nghiệm làm ở Nhật, hoặc ở công ty Nhật lâu năm càng tốt</div><div>Năng lực chuyên môn</div><div>•Có bằng cấp liên quan đến kết cấu thép Nhật càng tốt</div><div>•Kỹ năng vẽ CAD cơ bản, Sử dụng thành thạo Tekla hoặc phần mềm real 4</div><div>•Đọc hiểu bản vẽ lắp dựng, bản vẽ tổng thể, tiêu chuẩn đường hàn, tiêu chuẩn JASS 6, ASTM , …</div><div><br></div><div>Năng lực bổ trợ</div><div>• Kỹ năng giao tiếp</div><div>• Kỹ năng giải quyết vấn đề và ra quyết định</div><div>• Kỹ năng làm việc nhóm</div><div>• Kỹ năng sử dụng thành thạo tin học văn phòng</div><div><br></div><div>4. Thái độ hoặc tố chất</div><div>• Có tinh thần trách nhiệm</div><div>• Cẩn thận, tỉ mỉ, chủ động, cầu tiến</div><div><br></div><div>5. Yêu cầu khác:</div><div>•Có đủ sức khỏe để đáp ứng yêu cầu nhiệm vụ được giao</div><div>•Có thể đi công tác</div>",
                StarDate = new DateTime(2023, 3, 15),
                EndDate = new DateTime(2023, 5, 15),
                Status = Enum.Status.SUCCESS,
                Place = Enum.Place._20,
                PostCategories = Enum.PostCategories.CO_DIEN,
                Salaries = "+600000",
                NumberPeople = 20,
                ContractorID = 1,
                CreateBy = Guid.Parse("d7285fb7-835b-4680-a18c-673bd71f63d7")
            });
            modelBuilder.Entity<ContractorPostSkill>().HasData(new ContractorPostSkill
            {
                ContractorPostID = 4,
                SkillID = 2
            });
            modelBuilder.Entity<ContractorPostSkill>().HasData(new ContractorPostSkill
            {
                ContractorPostID = 4,
                SkillID = 3
            });
            #endregion

            #region Contractor Post 5
            modelBuilder.Entity<ContractorPost>().HasData(new ContractorPost
            {
                Id = 5,
                Title = "Tuyển dụng công nhân xây dựng",
                ProjectName = "TPHCM - QUẬN 7 - TUYỂN DỤNG GẤP",
                Description = "<div>I. THÔNG TIN CHUNG</div><div>1. Quy trình công việc liên quan: Quy trình quản lí dự án (mảng bản vẽ, báo giá, tiến độ)</div><div>2. Cấp trực tiếp quản lý: Giám đốc dự án</div><div>3. Loại hợp đồng: Hợp đồng xác định có thời hạn/không thời hạn</div><div><br></div><div>II. MỤC ĐÍCH CÔNG VIỆC</div><div>Nắm bản vẽ của dự án từ lúc đấu thầu, hiểu rõ các spect của dự án để báo giá. Có khả năng bốc khối lượng để đối ứng với cá báo giá gấp, các hạng mục phát sinh. Khi dự án trúng thấu, có khả năng điều phối dự án ở vai trò quản lí thiết kế, quản lí tiến độ, hoặc quản lí chất lượng (đối với dự án quy mô nhỏ)</div><div><br></div><div>III. TRÁCH NHIỆM VÀ NHIỆM VỤ</div><div>1. Làm báo giá dự án Nhật và hỗ trợ giám đốc dự án đi đấu thầu</div><div>• Nắm rõ bản vẽ của dự án từ lúc đấu thầu</div><div>• Hiểu rõ các vật tư spect của ngành kết cấu thép để báo giá</div><div>• Có khả năng bốc khối lượng cho các dự án gấp</div><div>• Có khả năng lên các bản vẽ đề xuất bằng CAD</div><div>• Cùng với GDDA đi đấu thầu các dự án</div><div>• Tiếp khách, đối ứng khách khi có audit, khách về việt nam</div><div><br></div><div>2. Quản lí dự án trúng thầu mảng quản lí bản vẽ thiết kế, quản lí tiến độ</div><div>• Nắm rõ các thay đổi thiết kế, chỉ thị bản vẽ của khách để triển khai cho shop</div><div>• Sử dụng thành thạo Tekla hoặc phần mềm real 4 để xuất các giấy tờ phục vụ cho quản lí dự án</div><div>• Lên được kế hoạch sản xuất và quản lí sản xuất, xuất hàng</div><div><br></div><div>3. Đối ứng khách hàng</div><div>• Báo cáo tiến độ sản xuất, bản vẽ</div><div>• Tham gia chủ đạo trong các cuộc họp tiến độ với nhà máy</div><div><br></div><div>4 . Nghiên cứu &amp; Phát triển</div><div>• Tham gia nghiên cứu và phát triển giải pháp công nghệ sản xuất</div><div><br></div><div>5. Tổng kết và đúc kết kinh nghiệm quản lí dự án</div><div>• Tổng kết thường xuyên các vướng mắt trong tiến độ, bản vẽ . Các lỗi hay mắc phải, hướng xử lí</div><div>• Đối với NCR đóng vai trò chỉ huy xử lí (liên quan tiến độ, chất lượng)</div><div><br></div><div>6. Cost control</div><div>• Phối hợp với giám đốc dự án trong công tác giám sát ngân sách thực hiện</div><div><br></div><div>7. Nhiệm vụ khác</div><div>• Các nhiệm vụ khác được phân công</div>",
                Benefit = "<div>- Tăng lương 6 tháng một lần. Thưởng vào các ngày lễ tết. Cơ hội thăng tiến cao.</div><div>- Xe đưa rước hàng ngày</div><div>- Khám sức khỏe &amp; đi du lịch và các chế độ khác theo quy định của pháp luật Lao động</div>",
                Required = "<div>1. Trình độ đào tạo:</div><div>• Bằng cấp tối thiểu: Cao đẳng, Đại học</div><div>• Chuyên ngành đào tạo: Xây dựng Dân dụng và Công nghiệp, Cơ khí</div><div>• Chứng chỉ, giấy phép hành nghề (nếu có): không yêu cầu.Ưu tiên nếu có chứng chỉ:….</div><div>• Trình độ Ngoại ngữ (tiếng anh, tiếng nhật, tiếng trung): Tiếng Nhật (bắt buộc, N2 đổ lên )</div><div><br></div><div>2. Kiến thức cần có:</div><div>• Sản phẩm dịch vụ của Công ty</div><div>• Kiến thức về lĩnh vực kết cấu thép (tiêu chuẩn, vật liệu, sản xuất, quy trình thi công, lắp dựng)</div><div><br></div><div>3. Năng lực cần thiết:</div><div>Năng lực quản lý</div><div>•Nếu có kinh nghiệm làm ở Nhật, hoặc ở công ty Nhật lâu năm càng tốt</div><div>Năng lực chuyên môn</div><div>•Có bằng cấp liên quan đến kết cấu thép Nhật càng tốt</div><div>•Kỹ năng vẽ CAD cơ bản, Sử dụng thành thạo Tekla hoặc phần mềm real 4</div><div>•Đọc hiểu bản vẽ lắp dựng, bản vẽ tổng thể, tiêu chuẩn đường hàn, tiêu chuẩn JASS 6, ASTM , …</div><div><br></div><div>Năng lực bổ trợ</div><div>• Kỹ năng giao tiếp</div><div>• Kỹ năng giải quyết vấn đề và ra quyết định</div><div>• Kỹ năng làm việc nhóm</div><div>• Kỹ năng sử dụng thành thạo tin học văn phòng</div><div><br></div><div>4. Thái độ hoặc tố chất</div><div>• Có tinh thần trách nhiệm</div><div>• Cẩn thận, tỉ mỉ, chủ động, cầu tiến</div><div><br></div><div>5. Yêu cầu khác:</div><div>•Có đủ sức khỏe để đáp ứng yêu cầu nhiệm vụ được giao</div><div>•Có thể đi công tác</div>",
                StarDate = new DateTime(2023, 5, 15),
                EndDate = new DateTime(2024, 5, 15),
                Status = Enum.Status.SUCCESS,
                Place = Enum.Place._14,
                PostCategories = Enum.PostCategories.HIEN_DAI,
                Salaries = "+600000",
                NumberPeople = 30,
                CreateBy = Guid.Parse("d7285fb7-835b-4680-a18c-673bd71f63d6"),
                ContractorID = 2
            });

            modelBuilder.Entity<ContractorPostSkill>().HasData(new ContractorPostSkill
            {
                ContractorPostID = 5,
                SkillID = 1
            });
            #endregion

            #region Contractor Post 6
            modelBuilder.Entity<ContractorPost>().HasData(new ContractorPost
            {
                Id = 6,
                Title = "Tuyển dụng công nhân xây dựng",
                ProjectName = "TPHCM - QUẬN 7 - TUYỂN DỤNG GẤP",
                Description = "<div>I. THÔNG TIN CHUNG</div><div>1. Quy trình công việc liên quan: Quy trình quản lí dự án (mảng bản vẽ, báo giá, tiến độ)</div><div>2. Cấp trực tiếp quản lý: Giám đốc dự án</div><div>3. Loại hợp đồng: Hợp đồng xác định có thời hạn/không thời hạn</div><div><br></div><div>II. MỤC ĐÍCH CÔNG VIỆC</div><div>Nắm bản vẽ của dự án từ lúc đấu thầu, hiểu rõ các spect của dự án để báo giá. Có khả năng bốc khối lượng để đối ứng với cá báo giá gấp, các hạng mục phát sinh. Khi dự án trúng thấu, có khả năng điều phối dự án ở vai trò quản lí thiết kế, quản lí tiến độ, hoặc quản lí chất lượng (đối với dự án quy mô nhỏ)</div><div><br></div><div>III. TRÁCH NHIỆM VÀ NHIỆM VỤ</div><div>1. Làm báo giá dự án Nhật và hỗ trợ giám đốc dự án đi đấu thầu</div><div>• Nắm rõ bản vẽ của dự án từ lúc đấu thầu</div><div>• Hiểu rõ các vật tư spect của ngành kết cấu thép để báo giá</div><div>• Có khả năng bốc khối lượng cho các dự án gấp</div><div>• Có khả năng lên các bản vẽ đề xuất bằng CAD</div><div>• Cùng với GDDA đi đấu thầu các dự án</div><div>• Tiếp khách, đối ứng khách khi có audit, khách về việt nam</div><div><br></div><div>2. Quản lí dự án trúng thầu mảng quản lí bản vẽ thiết kế, quản lí tiến độ</div><div>• Nắm rõ các thay đổi thiết kế, chỉ thị bản vẽ của khách để triển khai cho shop</div><div>• Sử dụng thành thạo Tekla hoặc phần mềm real 4 để xuất các giấy tờ phục vụ cho quản lí dự án</div><div>• Lên được kế hoạch sản xuất và quản lí sản xuất, xuất hàng</div><div><br></div><div>3. Đối ứng khách hàng</div><div>• Báo cáo tiến độ sản xuất, bản vẽ</div><div>• Tham gia chủ đạo trong các cuộc họp tiến độ với nhà máy</div><div><br></div><div>4 . Nghiên cứu &amp; Phát triển</div><div>• Tham gia nghiên cứu và phát triển giải pháp công nghệ sản xuất</div><div><br></div><div>5. Tổng kết và đúc kết kinh nghiệm quản lí dự án</div><div>• Tổng kết thường xuyên các vướng mắt trong tiến độ, bản vẽ . Các lỗi hay mắc phải, hướng xử lí</div><div>• Đối với NCR đóng vai trò chỉ huy xử lí (liên quan tiến độ, chất lượng)</div><div><br></div><div>6. Cost control</div><div>• Phối hợp với giám đốc dự án trong công tác giám sát ngân sách thực hiện</div><div><br></div><div>7. Nhiệm vụ khác</div><div>• Các nhiệm vụ khác được phân công</div>",
                Benefit = "<div>- Tăng lương 6 tháng một lần. Thưởng vào các ngày lễ tết. Cơ hội thăng tiến cao.</div><div>- Xe đưa rước hàng ngày</div><div>- Khám sức khỏe &amp; đi du lịch và các chế độ khác theo quy định của pháp luật Lao động</div>",
                Required = "<div>1. Trình độ đào tạo:</div><div>• Bằng cấp tối thiểu: Cao đẳng, Đại học</div><div>• Chuyên ngành đào tạo: Xây dựng Dân dụng và Công nghiệp, Cơ khí</div><div>• Chứng chỉ, giấy phép hành nghề (nếu có): không yêu cầu.Ưu tiên nếu có chứng chỉ:….</div><div>• Trình độ Ngoại ngữ (tiếng anh, tiếng nhật, tiếng trung): Tiếng Nhật (bắt buộc, N2 đổ lên )</div><div><br></div><div>2. Kiến thức cần có:</div><div>• Sản phẩm dịch vụ của Công ty</div><div>• Kiến thức về lĩnh vực kết cấu thép (tiêu chuẩn, vật liệu, sản xuất, quy trình thi công, lắp dựng)</div><div><br></div><div>3. Năng lực cần thiết:</div><div>Năng lực quản lý</div><div>•Nếu có kinh nghiệm làm ở Nhật, hoặc ở công ty Nhật lâu năm càng tốt</div><div>Năng lực chuyên môn</div><div>•Có bằng cấp liên quan đến kết cấu thép Nhật càng tốt</div><div>•Kỹ năng vẽ CAD cơ bản, Sử dụng thành thạo Tekla hoặc phần mềm real 4</div><div>•Đọc hiểu bản vẽ lắp dựng, bản vẽ tổng thể, tiêu chuẩn đường hàn, tiêu chuẩn JASS 6, ASTM , …</div><div><br></div><div>Năng lực bổ trợ</div><div>• Kỹ năng giao tiếp</div><div>• Kỹ năng giải quyết vấn đề và ra quyết định</div><div>• Kỹ năng làm việc nhóm</div><div>• Kỹ năng sử dụng thành thạo tin học văn phòng</div><div><br></div><div>4. Thái độ hoặc tố chất</div><div>• Có tinh thần trách nhiệm</div><div>• Cẩn thận, tỉ mỉ, chủ động, cầu tiến</div><div><br></div><div>5. Yêu cầu khác:</div><div>•Có đủ sức khỏe để đáp ứng yêu cầu nhiệm vụ được giao</div><div>•Có thể đi công tác</div>",
                StarDate = new DateTime(2023, 6, 15),
                EndDate = new DateTime(2024, 5, 30),
                Status = Enum.Status.SUCCESS,
                Place = Enum.Place._14,
                PostCategories = Enum.PostCategories.HIEN_DAI,
                Salaries = "+600000",
                NumberPeople = 30,
                ContractorID = 1,
                CreateBy = Guid.Parse("d7285fb7-835b-4680-a18c-673bd71f63d7")

            });

            modelBuilder.Entity<ContractorPostSkill>().HasData(new ContractorPostSkill
            {
                ContractorPostID = 6,
                SkillID = 4
            });
            #endregion

            #region Contractor Post 7
            modelBuilder.Entity<ContractorPost>().HasData(new ContractorPost
            {
                Id = 7,
                Title = "Tuyển dụng nam lao động xây dựng cao cấp",
                ProjectName = "TPHCM - QUẬN 10 - TUYỂN DỤNG GẤP",
                Description = "<div>I. THÔNG TIN CHUNG</div><div>1. Quy trình công việc liên quan: Quy trình quản lí dự án (mảng bản vẽ, báo giá, tiến độ)</div><div>2. Cấp trực tiếp quản lý: Giám đốc dự án</div><div>3. Loại hợp đồng: Hợp đồng xác định có thời hạn/không thời hạn</div><div><br></div><div>II. MỤC ĐÍCH CÔNG VIỆC</div><div>Nắm bản vẽ của dự án từ lúc đấu thầu, hiểu rõ các spect của dự án để báo giá. Có khả năng bốc khối lượng để đối ứng với cá báo giá gấp, các hạng mục phát sinh. Khi dự án trúng thấu, có khả năng điều phối dự án ở vai trò quản lí thiết kế, quản lí tiến độ, hoặc quản lí chất lượng (đối với dự án quy mô nhỏ)</div><div><br></div><div>III. TRÁCH NHIỆM VÀ NHIỆM VỤ</div><div>1. Làm báo giá dự án Nhật và hỗ trợ giám đốc dự án đi đấu thầu</div><div>• Nắm rõ bản vẽ của dự án từ lúc đấu thầu</div><div>• Hiểu rõ các vật tư spect của ngành kết cấu thép để báo giá</div><div>• Có khả năng bốc khối lượng cho các dự án gấp</div><div>• Có khả năng lên các bản vẽ đề xuất bằng CAD</div><div>• Cùng với GDDA đi đấu thầu các dự án</div><div>• Tiếp khách, đối ứng khách khi có audit, khách về việt nam</div><div><br></div><div>2. Quản lí dự án trúng thầu mảng quản lí bản vẽ thiết kế, quản lí tiến độ</div><div>• Nắm rõ các thay đổi thiết kế, chỉ thị bản vẽ của khách để triển khai cho shop</div><div>• Sử dụng thành thạo Tekla hoặc phần mềm real 4 để xuất các giấy tờ phục vụ cho quản lí dự án</div><div>• Lên được kế hoạch sản xuất và quản lí sản xuất, xuất hàng</div><div><br></div><div>3. Đối ứng khách hàng</div><div>• Báo cáo tiến độ sản xuất, bản vẽ</div><div>• Tham gia chủ đạo trong các cuộc họp tiến độ với nhà máy</div><div><br></div><div>4 . Nghiên cứu &amp; Phát triển</div><div>• Tham gia nghiên cứu và phát triển giải pháp công nghệ sản xuất</div><div><br></div><div>5. Tổng kết và đúc kết kinh nghiệm quản lí dự án</div><div>• Tổng kết thường xuyên các vướng mắt trong tiến độ, bản vẽ . Các lỗi hay mắc phải, hướng xử lí</div><div>• Đối với NCR đóng vai trò chỉ huy xử lí (liên quan tiến độ, chất lượng)</div><div><br></div><div>6. Cost control</div><div>• Phối hợp với giám đốc dự án trong công tác giám sát ngân sách thực hiện</div><div><br></div><div>7. Nhiệm vụ khác</div><div>• Các nhiệm vụ khác được phân công</div>",
                Benefit = "<div>- Tăng lương 6 tháng một lần. Thưởng vào các ngày lễ tết. Cơ hội thăng tiến cao.</div><div>- Xe đưa rước hàng ngày</div><div>- Khám sức khỏe &amp; đi du lịch và các chế độ khác theo quy định của pháp luật Lao động</div>",
                Required = "<div>1. Trình độ đào tạo:</div><div>• Bằng cấp tối thiểu: Cao đẳng, Đại học</div><div>• Chuyên ngành đào tạo: Xây dựng Dân dụng và Công nghiệp, Cơ khí</div><div>• Chứng chỉ, giấy phép hành nghề (nếu có): không yêu cầu.Ưu tiên nếu có chứng chỉ:….</div><div>• Trình độ Ngoại ngữ (tiếng anh, tiếng nhật, tiếng trung): Tiếng Nhật (bắt buộc, N2 đổ lên )</div><div><br></div><div>2. Kiến thức cần có:</div><div>• Sản phẩm dịch vụ của Công ty</div><div>• Kiến thức về lĩnh vực kết cấu thép (tiêu chuẩn, vật liệu, sản xuất, quy trình thi công, lắp dựng)</div><div><br></div><div>3. Năng lực cần thiết:</div><div>Năng lực quản lý</div><div>•Nếu có kinh nghiệm làm ở Nhật, hoặc ở công ty Nhật lâu năm càng tốt</div><div>Năng lực chuyên môn</div><div>•Có bằng cấp liên quan đến kết cấu thép Nhật càng tốt</div><div>•Kỹ năng vẽ CAD cơ bản, Sử dụng thành thạo Tekla hoặc phần mềm real 4</div><div>•Đọc hiểu bản vẽ lắp dựng, bản vẽ tổng thể, tiêu chuẩn đường hàn, tiêu chuẩn JASS 6, ASTM , …</div><div><br></div><div>Năng lực bổ trợ</div><div>• Kỹ năng giao tiếp</div><div>• Kỹ năng giải quyết vấn đề và ra quyết định</div><div>• Kỹ năng làm việc nhóm</div><div>• Kỹ năng sử dụng thành thạo tin học văn phòng</div><div><br></div><div>4. Thái độ hoặc tố chất</div><div>• Có tinh thần trách nhiệm</div><div>• Cẩn thận, tỉ mỉ, chủ động, cầu tiến</div><div><br></div><div>5. Yêu cầu khác:</div><div>•Có đủ sức khỏe để đáp ứng yêu cầu nhiệm vụ được giao</div><div>•Có thể đi công tác</div>",
                StarDate = new DateTime(2023, 3, 15),
                EndDate = new DateTime(2023, 5, 15),
                Status = Enum.Status.SUCCESS,
                Place = Enum.Place._52,
                PostCategories = Enum.PostCategories.CO_DIEN,
                Salaries = "+600000",
                NumberPeople = 20,
                ContractorID = 1,
                Accommodation = true,
                ConstructionType = "Nhà ở",
                Transport = true,
                StartTime = "9:00",
                EndTime = "16:30",
                CreateBy = Guid.Parse("d7285fb7-835b-4680-a18c-673bd71f63d7"),
                QuizRequired = false
            });

            modelBuilder.Entity<ContractorPostType>().HasData(new ContractorPostType
            {
                ContractorPostID = 7,
                TypeID = Guid.Parse("4ace8fcb-95eb-48c0-9deb-240e8b4e10e0")
            });

            modelBuilder.Entity<ContractorPostType>().HasData(new ContractorPostType
            {
                ContractorPostID = 7,
                TypeID = Guid.Parse("ce9fa65b-d005-46b6-953e-e6462a59cfb3")
            });


            modelBuilder.Entity<ContractorPostSkill>().HasData(new ContractorPostSkill
            {
                ContractorPostID = 7,
                SkillID = 1
            });

            modelBuilder.Entity<ContractorPostSkill>().HasData(new ContractorPostSkill
            {
                ContractorPostID = 7,
                SkillID = 2
            });
            #endregion

            #region Contractor Post 8
            modelBuilder.Entity<ContractorPost>().HasData(new ContractorPost
            {
                Id = 8,
                Title = "Tuyển dụng nam lao động ",
                ProjectName = "TPHCM - TUYỂN DỤNG GẤP",
                Description = "<div>I. THÔNG TIN CHUNG</div><div>1. Quy trình công việc liên quan: Quy trình quản lí dự án (mảng bản vẽ, báo giá, tiến độ)</div><div>2. Cấp trực tiếp quản lý: Giám đốc dự án</div><div>3. Loại hợp đồng: Hợp đồng xác định có thời hạn/không thời hạn</div><div><br></div><div>II. MỤC ĐÍCH CÔNG VIỆC</div><div>Nắm bản vẽ của dự án từ lúc đấu thầu, hiểu rõ các spect của dự án để báo giá. Có khả năng bốc khối lượng để đối ứng với cá báo giá gấp, các hạng mục phát sinh. Khi dự án trúng thấu, có khả năng điều phối dự án ở vai trò quản lí thiết kế, quản lí tiến độ, hoặc quản lí chất lượng (đối với dự án quy mô nhỏ)</div><div><br></div><div>III. TRÁCH NHIỆM VÀ NHIỆM VỤ</div><div>1. Làm báo giá dự án Nhật và hỗ trợ giám đốc dự án đi đấu thầu</div><div>• Nắm rõ bản vẽ của dự án từ lúc đấu thầu</div><div>• Hiểu rõ các vật tư spect của ngành kết cấu thép để báo giá</div><div>• Có khả năng bốc khối lượng cho các dự án gấp</div><div>• Có khả năng lên các bản vẽ đề xuất bằng CAD</div><div>• Cùng với GDDA đi đấu thầu các dự án</div><div>• Tiếp khách, đối ứng khách khi có audit, khách về việt nam</div><div><br></div><div>2. Quản lí dự án trúng thầu mảng quản lí bản vẽ thiết kế, quản lí tiến độ</div><div>• Nắm rõ các thay đổi thiết kế, chỉ thị bản vẽ của khách để triển khai cho shop</div><div>• Sử dụng thành thạo Tekla hoặc phần mềm real 4 để xuất các giấy tờ phục vụ cho quản lí dự án</div><div>• Lên được kế hoạch sản xuất và quản lí sản xuất, xuất hàng</div><div><br></div><div>3. Đối ứng khách hàng</div><div>• Báo cáo tiến độ sản xuất, bản vẽ</div><div>• Tham gia chủ đạo trong các cuộc họp tiến độ với nhà máy</div><div><br></div><div>4 . Nghiên cứu &amp; Phát triển</div><div>• Tham gia nghiên cứu và phát triển giải pháp công nghệ sản xuất</div><div><br></div><div>5. Tổng kết và đúc kết kinh nghiệm quản lí dự án</div><div>• Tổng kết thường xuyên các vướng mắt trong tiến độ, bản vẽ . Các lỗi hay mắc phải, hướng xử lí</div><div>• Đối với NCR đóng vai trò chỉ huy xử lí (liên quan tiến độ, chất lượng)</div><div><br></div><div>6. Cost control</div><div>• Phối hợp với giám đốc dự án trong công tác giám sát ngân sách thực hiện</div><div><br></div><div>7. Nhiệm vụ khác</div><div>• Các nhiệm vụ khác được phân công</div>",
                Benefit = "<div>- Tăng lương 6 tháng một lần. Thưởng vào các ngày lễ tết. Cơ hội thăng tiến cao.</div><div>- Xe đưa rước hàng ngày</div><div>- Khám sức khỏe &amp; đi du lịch và các chế độ khác theo quy định của pháp luật Lao động</div>",
                Required = "<div>1. Trình độ đào tạo:</div><div>• Bằng cấp tối thiểu: Cao đẳng, Đại học</div><div>• Chuyên ngành đào tạo: Xây dựng Dân dụng và Công nghiệp, Cơ khí</div><div>• Chứng chỉ, giấy phép hành nghề (nếu có): không yêu cầu.Ưu tiên nếu có chứng chỉ:….</div><div>• Trình độ Ngoại ngữ (tiếng anh, tiếng nhật, tiếng trung): Tiếng Nhật (bắt buộc, N2 đổ lên )</div><div><br></div><div>2. Kiến thức cần có:</div><div>• Sản phẩm dịch vụ của Công ty</div><div>• Kiến thức về lĩnh vực kết cấu thép (tiêu chuẩn, vật liệu, sản xuất, quy trình thi công, lắp dựng)</div><div><br></div><div>3. Năng lực cần thiết:</div><div>Năng lực quản lý</div><div>•Nếu có kinh nghiệm làm ở Nhật, hoặc ở công ty Nhật lâu năm càng tốt</div><div>Năng lực chuyên môn</div><div>•Có bằng cấp liên quan đến kết cấu thép Nhật càng tốt</div><div>•Kỹ năng vẽ CAD cơ bản, Sử dụng thành thạo Tekla hoặc phần mềm real 4</div><div>•Đọc hiểu bản vẽ lắp dựng, bản vẽ tổng thể, tiêu chuẩn đường hàn, tiêu chuẩn JASS 6, ASTM , …</div><div><br></div><div>Năng lực bổ trợ</div><div>• Kỹ năng giao tiếp</div><div>• Kỹ năng giải quyết vấn đề và ra quyết định</div><div>• Kỹ năng làm việc nhóm</div><div>• Kỹ năng sử dụng thành thạo tin học văn phòng</div><div><br></div><div>4. Thái độ hoặc tố chất</div><div>• Có tinh thần trách nhiệm</div><div>• Cẩn thận, tỉ mỉ, chủ động, cầu tiến</div><div><br></div><div>5. Yêu cầu khác:</div><div>•Có đủ sức khỏe để đáp ứng yêu cầu nhiệm vụ được giao</div><div>•Có thể đi công tác</div>",
                StarDate = new DateTime(2023, 3, 15),
                EndDate = new DateTime(2023, 5, 15),
                Status = Enum.Status.SUCCESS,
                Place = Enum.Place._52,
                PostCategories = Enum.PostCategories.HIEN_DAI,
                Salaries = "+600000",
                NumberPeople = 20,
                ContractorID = 1,
                Accommodation = true,
                ConstructionType = "Nhà ở",
                Transport = true,
                StartTime = "9:00",
                EndTime = "16:30",
                CreateBy = Guid.Parse("d7285fb7-835b-4680-a18c-673bd71f63d7"),
                QuizRequired = false
            });

            modelBuilder.Entity<ContractorPostType>().HasData(new ContractorPostType
            {
                ContractorPostID = 8,
                TypeID = Guid.Parse("4ace8fcb-95eb-48c0-9deb-240e8b4e10e0")
            });

            modelBuilder.Entity<ContractorPostType>().HasData(new ContractorPostType
            {
                ContractorPostID = 8,
                TypeID = Guid.Parse("ce9fa65b-d005-46b6-953e-e6462a59cfb3")
            });


            modelBuilder.Entity<ContractorPostSkill>().HasData(new ContractorPostSkill
            {
                ContractorPostID = 8,
                SkillID = 1
            });

            #endregion

            #region Contractor Post 9 
            modelBuilder.Entity<ContractorPost>().HasData(new ContractorPost
            {
                Id = 9,
                Title = "Tuyển dụng xây dựng",
                ProjectName = "TPHCM - TUYỂN DỤNG GẤP",
                Description = "<div>I. THÔNG TIN CHUNG</div><div>1. Quy trình công việc liên quan: Quy trình quản lí dự án (mảng bản vẽ, báo giá, tiến độ)</div><div>2. Cấp trực tiếp quản lý: Giám đốc dự án</div><div>3. Loại hợp đồng: Hợp đồng xác định có thời hạn/không thời hạn</div><div><br></div><div>II. MỤC ĐÍCH CÔNG VIỆC</div><div>Nắm bản vẽ của dự án từ lúc đấu thầu, hiểu rõ các spect của dự án để báo giá. Có khả năng bốc khối lượng để đối ứng với cá báo giá gấp, các hạng mục phát sinh. Khi dự án trúng thấu, có khả năng điều phối dự án ở vai trò quản lí thiết kế, quản lí tiến độ, hoặc quản lí chất lượng (đối với dự án quy mô nhỏ)</div><div><br></div><div>III. TRÁCH NHIỆM VÀ NHIỆM VỤ</div><div>1. Làm báo giá dự án Nhật và hỗ trợ giám đốc dự án đi đấu thầu</div><div>• Nắm rõ bản vẽ của dự án từ lúc đấu thầu</div><div>• Hiểu rõ các vật tư spect của ngành kết cấu thép để báo giá</div><div>• Có khả năng bốc khối lượng cho các dự án gấp</div><div>• Có khả năng lên các bản vẽ đề xuất bằng CAD</div><div>• Cùng với GDDA đi đấu thầu các dự án</div><div>• Tiếp khách, đối ứng khách khi có audit, khách về việt nam</div><div><br></div><div>2. Quản lí dự án trúng thầu mảng quản lí bản vẽ thiết kế, quản lí tiến độ</div><div>• Nắm rõ các thay đổi thiết kế, chỉ thị bản vẽ của khách để triển khai cho shop</div><div>• Sử dụng thành thạo Tekla hoặc phần mềm real 4 để xuất các giấy tờ phục vụ cho quản lí dự án</div><div>• Lên được kế hoạch sản xuất và quản lí sản xuất, xuất hàng</div><div><br></div><div>3. Đối ứng khách hàng</div><div>• Báo cáo tiến độ sản xuất, bản vẽ</div><div>• Tham gia chủ đạo trong các cuộc họp tiến độ với nhà máy</div><div><br></div><div>4 . Nghiên cứu &amp; Phát triển</div><div>• Tham gia nghiên cứu và phát triển giải pháp công nghệ sản xuất</div><div><br></div><div>5. Tổng kết và đúc kết kinh nghiệm quản lí dự án</div><div>• Tổng kết thường xuyên các vướng mắt trong tiến độ, bản vẽ . Các lỗi hay mắc phải, hướng xử lí</div><div>• Đối với NCR đóng vai trò chỉ huy xử lí (liên quan tiến độ, chất lượng)</div><div><br></div><div>6. Cost control</div><div>• Phối hợp với giám đốc dự án trong công tác giám sát ngân sách thực hiện</div><div><br></div><div>7. Nhiệm vụ khác</div><div>• Các nhiệm vụ khác được phân công</div>",
                Benefit = "<div>- Tăng lương 6 tháng một lần. Thưởng vào các ngày lễ tết. Cơ hội thăng tiến cao.</div><div>- Xe đưa rước hàng ngày</div><div>- Khám sức khỏe &amp; đi du lịch và các chế độ khác theo quy định của pháp luật Lao động</div>",
                Required = "<div>1. Trình độ đào tạo:</div><div>• Bằng cấp tối thiểu: Cao đẳng, Đại học</div><div>• Chuyên ngành đào tạo: Xây dựng Dân dụng và Công nghiệp, Cơ khí</div><div>• Chứng chỉ, giấy phép hành nghề (nếu có): không yêu cầu.Ưu tiên nếu có chứng chỉ:….</div><div>• Trình độ Ngoại ngữ (tiếng anh, tiếng nhật, tiếng trung): Tiếng Nhật (bắt buộc, N2 đổ lên )</div><div><br></div><div>2. Kiến thức cần có:</div><div>• Sản phẩm dịch vụ của Công ty</div><div>• Kiến thức về lĩnh vực kết cấu thép (tiêu chuẩn, vật liệu, sản xuất, quy trình thi công, lắp dựng)</div><div><br></div><div>3. Năng lực cần thiết:</div><div>Năng lực quản lý</div><div>•Nếu có kinh nghiệm làm ở Nhật, hoặc ở công ty Nhật lâu năm càng tốt</div><div>Năng lực chuyên môn</div><div>•Có bằng cấp liên quan đến kết cấu thép Nhật càng tốt</div><div>•Kỹ năng vẽ CAD cơ bản, Sử dụng thành thạo Tekla hoặc phần mềm real 4</div><div>•Đọc hiểu bản vẽ lắp dựng, bản vẽ tổng thể, tiêu chuẩn đường hàn, tiêu chuẩn JASS 6, ASTM , …</div><div><br></div><div>Năng lực bổ trợ</div><div>• Kỹ năng giao tiếp</div><div>• Kỹ năng giải quyết vấn đề và ra quyết định</div><div>• Kỹ năng làm việc nhóm</div><div>• Kỹ năng sử dụng thành thạo tin học văn phòng</div><div><br></div><div>4. Thái độ hoặc tố chất</div><div>• Có tinh thần trách nhiệm</div><div>• Cẩn thận, tỉ mỉ, chủ động, cầu tiến</div><div><br></div><div>5. Yêu cầu khác:</div><div>•Có đủ sức khỏe để đáp ứng yêu cầu nhiệm vụ được giao</div><div>•Có thể đi công tác</div>",
                StarDate = new DateTime(2023, 3, 15),
                EndDate = new DateTime(2023, 5, 15),
                Status = Enum.Status.SUCCESS,
                Place = Enum.Place._52,
                PostCategories = Enum.PostCategories.HIEN_DAI,
                Salaries = "+600000",
                NumberPeople = 20,
                ContractorID = 1,
                Accommodation = true,
                ConstructionType = "Nhà ở",
                Transport = true,
                StartTime = "9:00",
                EndTime = "16:30",
                CreateBy = Guid.Parse("d7285fb7-835b-4680-a18c-673bd71f63d7"),
                QuizRequired = false
            });

            modelBuilder.Entity<ContractorPostType>().HasData(new ContractorPostType
            {
                ContractorPostID = 9,
                TypeID = Guid.Parse("4ace8fcb-95eb-48c0-9deb-240e8b4e10e0")
            });

            modelBuilder.Entity<ContractorPostType>().HasData(new ContractorPostType
            {
                ContractorPostID = 9,
                TypeID = Guid.Parse("ce9fa65b-d005-46b6-953e-e6462a59cfb3")
            });


            modelBuilder.Entity<ContractorPostSkill>().HasData(new ContractorPostSkill
            {
                ContractorPostID = 9,
                SkillID = 1
            });
            #endregion

            #region Contractor Post 10
            modelBuilder.Entity<ContractorPost>().HasData(new ContractorPost
            {
                Id = 10,
                Title = "Tuyển dụng xây dựng ",
                ProjectName = "TPHCM - TUYỂN DỤNG GẤP",
                Description = "<div>I. THÔNG TIN CHUNG</div><div>1. Quy trình công việc liên quan: Quy trình quản lí dự án (mảng bản vẽ, báo giá, tiến độ)</div><div>2. Cấp trực tiếp quản lý: Giám đốc dự án</div><div>3. Loại hợp đồng: Hợp đồng xác định có thời hạn/không thời hạn</div><div><br></div><div>II. MỤC ĐÍCH CÔNG VIỆC</div><div>Nắm bản vẽ của dự án từ lúc đấu thầu, hiểu rõ các spect của dự án để báo giá. Có khả năng bốc khối lượng để đối ứng với cá báo giá gấp, các hạng mục phát sinh. Khi dự án trúng thấu, có khả năng điều phối dự án ở vai trò quản lí thiết kế, quản lí tiến độ, hoặc quản lí chất lượng (đối với dự án quy mô nhỏ)</div><div><br></div><div>III. TRÁCH NHIỆM VÀ NHIỆM VỤ</div><div>1. Làm báo giá dự án Nhật và hỗ trợ giám đốc dự án đi đấu thầu</div><div>• Nắm rõ bản vẽ của dự án từ lúc đấu thầu</div><div>• Hiểu rõ các vật tư spect của ngành kết cấu thép để báo giá</div><div>• Có khả năng bốc khối lượng cho các dự án gấp</div><div>• Có khả năng lên các bản vẽ đề xuất bằng CAD</div><div>• Cùng với GDDA đi đấu thầu các dự án</div><div>• Tiếp khách, đối ứng khách khi có audit, khách về việt nam</div><div><br></div><div>2. Quản lí dự án trúng thầu mảng quản lí bản vẽ thiết kế, quản lí tiến độ</div><div>• Nắm rõ các thay đổi thiết kế, chỉ thị bản vẽ của khách để triển khai cho shop</div><div>• Sử dụng thành thạo Tekla hoặc phần mềm real 4 để xuất các giấy tờ phục vụ cho quản lí dự án</div><div>• Lên được kế hoạch sản xuất và quản lí sản xuất, xuất hàng</div><div><br></div><div>3. Đối ứng khách hàng</div><div>• Báo cáo tiến độ sản xuất, bản vẽ</div><div>• Tham gia chủ đạo trong các cuộc họp tiến độ với nhà máy</div><div><br></div><div>4 . Nghiên cứu &amp; Phát triển</div><div>• Tham gia nghiên cứu và phát triển giải pháp công nghệ sản xuất</div><div><br></div><div>5. Tổng kết và đúc kết kinh nghiệm quản lí dự án</div><div>• Tổng kết thường xuyên các vướng mắt trong tiến độ, bản vẽ . Các lỗi hay mắc phải, hướng xử lí</div><div>• Đối với NCR đóng vai trò chỉ huy xử lí (liên quan tiến độ, chất lượng)</div><div><br></div><div>6. Cost control</div><div>• Phối hợp với giám đốc dự án trong công tác giám sát ngân sách thực hiện</div><div><br></div><div>7. Nhiệm vụ khác</div><div>• Các nhiệm vụ khác được phân công</div>",
                Benefit = "<div>- Tăng lương 6 tháng một lần. Thưởng vào các ngày lễ tết. Cơ hội thăng tiến cao.</div><div>- Xe đưa rước hàng ngày</div><div>- Khám sức khỏe &amp; đi du lịch và các chế độ khác theo quy định của pháp luật Lao động</div>",
                Required = "<div>1. Trình độ đào tạo:</div><div>• Bằng cấp tối thiểu: Cao đẳng, Đại học</div><div>• Chuyên ngành đào tạo: Xây dựng Dân dụng và Công nghiệp, Cơ khí</div><div>• Chứng chỉ, giấy phép hành nghề (nếu có): không yêu cầu.Ưu tiên nếu có chứng chỉ:….</div><div>• Trình độ Ngoại ngữ (tiếng anh, tiếng nhật, tiếng trung): Tiếng Nhật (bắt buộc, N2 đổ lên )</div><div><br></div><div>2. Kiến thức cần có:</div><div>• Sản phẩm dịch vụ của Công ty</div><div>• Kiến thức về lĩnh vực kết cấu thép (tiêu chuẩn, vật liệu, sản xuất, quy trình thi công, lắp dựng)</div><div><br></div><div>3. Năng lực cần thiết:</div><div>Năng lực quản lý</div><div>•Nếu có kinh nghiệm làm ở Nhật, hoặc ở công ty Nhật lâu năm càng tốt</div><div>Năng lực chuyên môn</div><div>•Có bằng cấp liên quan đến kết cấu thép Nhật càng tốt</div><div>•Kỹ năng vẽ CAD cơ bản, Sử dụng thành thạo Tekla hoặc phần mềm real 4</div><div>•Đọc hiểu bản vẽ lắp dựng, bản vẽ tổng thể, tiêu chuẩn đường hàn, tiêu chuẩn JASS 6, ASTM , …</div><div><br></div><div>Năng lực bổ trợ</div><div>• Kỹ năng giao tiếp</div><div>• Kỹ năng giải quyết vấn đề và ra quyết định</div><div>• Kỹ năng làm việc nhóm</div><div>• Kỹ năng sử dụng thành thạo tin học văn phòng</div><div><br></div><div>4. Thái độ hoặc tố chất</div><div>• Có tinh thần trách nhiệm</div><div>• Cẩn thận, tỉ mỉ, chủ động, cầu tiến</div><div><br></div><div>5. Yêu cầu khác:</div><div>•Có đủ sức khỏe để đáp ứng yêu cầu nhiệm vụ được giao</div><div>•Có thể đi công tác</div>",
                StarDate = new DateTime(2023, 3, 15),
                EndDate = new DateTime(2023, 5, 15),
                Status = Enum.Status.SUCCESS,
                Place = Enum.Place._52,
                PostCategories = Enum.PostCategories.HIEN_DAI,
                Salaries = "+600000",
                NumberPeople = 20,
                ContractorID = 1,
                Accommodation = true,
                ConstructionType = "Nhà ở",
                Transport = true,
                StartTime = "9:00",
                EndTime = "16:30",
                CreateBy = Guid.Parse("d7285fb7-835b-4680-a18c-673bd71f63d7"),
                QuizRequired = false
            });

            modelBuilder.Entity<ContractorPostType>().HasData(new ContractorPostType
            {
                ContractorPostID = 10,
                TypeID = Guid.Parse("4ace8fcb-95eb-48c0-9deb-240e8b4e10e0")
            });

            modelBuilder.Entity<ContractorPostType>().HasData(new ContractorPostType
            {
                ContractorPostID = 10,
                TypeID = Guid.Parse("ce9fa65b-d005-46b6-953e-e6462a59cfb3")
            });


            modelBuilder.Entity<ContractorPostSkill>().HasData(new ContractorPostSkill
            {
                ContractorPostID = 10,
                SkillID = 1
            });
            #endregion

            #region Categories
            modelBuilder.Entity<Categories>().HasData(new Categories
            {

                ID = 1,
                Name = "Xuất xứ"
            });
            modelBuilder.Entity<Categories>().HasData(new Categories
            {

                ID = 2,
                Name = "Chất liệu"
            });
            modelBuilder.Entity<Categories>().HasData(new Categories
            {

                ID = 3,
                Name = "Phong cách "
            });
            modelBuilder.Entity<Categories>().HasData(new Categories
            {

                ID = 4,
                Name = "Vị trí "
            });
            #endregion

            #region Products
            //PRODUCT 1

            modelBuilder.Entity<Products>().HasData(new Products
            {
                Id = 7,
                Name = "Sơn Ngoại Thất Bóng Cao Cấp CMC ARMOS07 1 - 4.5L",
                UnitInStock = 200,
                UnitPrice = 857000,
                Image = "https://admin.mingstores.com/core/public/themes/mingstores/products/vx9kXzl3FacoKvdbZLki3kWM6nO3PimJ.jpg",
                SoldQuantities = 1500,
                Description = "- Màng sơn mịn có độ phủ cao, siêu bóng sang trọng,bám dính tốt\r\n\r\n- Hạn chế vết bẩn, vết nứt nhỏ, chống rêu mốc, độ bền màu cao\r\n\r\n- Thân thiện môi trường và an toàn cho sức khỏe\r\n\r\n- Bảo vệ 10 năm\r\n\r\n- Độ phủ lý thuyết: 12-14m2/lít/ lớp",
                MaterialStoreID = 1,
                Unit = "Lít",
                Brand = "CMC",
                LastModifiedAt = DateTime.Parse("2022/12/2"),
                Status = true
            });

            modelBuilder.Entity<ProductCategories>().HasData(new ProductCategories
            {
                CategoriesID = 1,
                ProductID = 7,
                Name = "Mỹ",

            });

            modelBuilder.Entity<ProductCategories>().HasData(new ProductCategories
            {
                CategoriesID = 2,
                ProductID = 7,
                Name = "Sơn ngoại thất",

            });

            modelBuilder.Entity<ProductSize>().HasData(new ProductSize
            {
                Id = 2,
                Name = "4.5L"

            });

            modelBuilder.Entity<ProductSize>().HasData(new ProductSize
            {
                Id = 3,
                Name = "7.5L"

            });

            modelBuilder.Entity<ProductType>().HasData(new ProductType
            {
                Id = 12,
                //Name = "Màu vàng",
                ProductID = 7,
                SizeID = 2,
                ColorId = 1,
                OtherID = 1,
                Quantity = 5,
                Status = Enum.Status.SUCCESS
            });

            modelBuilder.Entity<ProductType>().HasData(new ProductType
            {
                Id = 13,
                //Name = "Màu vàng",
                ProductID = 7,
                SizeID = 3,
                OtherID = 1,
                ColorId = 1,
                Quantity = 10,
                Status = Enum.Status.SUCCESS
            });




            //PRODUCT 2

            modelBuilder.Entity<Products>().HasData(new Products
            {
                Id = 30,
                Name = "Gạch GCA-Clay Art 60x60",
                UnitInStock = 200,
                UnitPrice = 400000,
                Image = "https://admin.mingstores.com/core/public/themes/mingstores/products/Elgda4SYGE52gAn2wi5AEXipIEMqiYiB.jpg",
                SoldQuantities = 1500,
                Description = "Gạch cao cấp đến từ thương hiệu nổi tiếng NIRO GRANITE",
                MaterialStoreID = 1,
                Unit = "Viên",
                Brand = "NIRO GRANITE",
                LastModifiedAt = DateTime.Parse("2022/1/2"),
                Status = true

            });

            modelBuilder.Entity<ProductCategories>().HasData(new ProductCategories
            {
                CategoriesID = 1,
                ProductID = 30,
                Name = "Mỹ",

            });

            modelBuilder.Entity<ProductCategories>().HasData(new ProductCategories
            {
                CategoriesID = 2,
                ProductID = 30,
                Name = "Gạch bóng",
            });

            //PRODUCT 3

            modelBuilder.Entity<Products>().HasData(new Products
            {
                Id = 31,
                Name = "Aures Smart Round RMC-45E-VN",
                UnitInStock = 200,
                UnitPrice = 3200000,
                Image = "https://admin.mingstores.com/core/public/themes/mingstores/products/MbcC070BBSf4q97sgghpjBbqFiNr7JEP.jpg",
                SoldQuantities = 1500,
                Description = "Công suất định mức: 4500(W)\r\nHình dáng: Hình tròn\r\nĐiện năng: 220V\r\nChế độ vòi sen: 5\r\nÁp lực nước tối thiểu: 30/0,3 Kpa/bar\r\nÁp lực nước tối đa: 380/3.8 Kpa/bar\r\nKích thước (DxCxR): 350 X 80\r\nTrọng lượng: 2 kg\r\nKhông có bơm trợ lực",
                MaterialStoreID = 1,
                Unit = "Bộ",
                Brand = "ARISTON",
                LastModifiedAt = DateTime.Parse("2023/1/2"),
                Status = true
            });

            modelBuilder.Entity<ProductCategories>().HasData(new ProductCategories
            {
                CategoriesID = 1,
                ProductID = 31,
                Name = "Mỹ",

            });

            modelBuilder.Entity<ProductCategories>().HasData(new ProductCategories
            {
                CategoriesID = 2,
                ProductID = 31,
                Name = "Thép",
            });


            //PRODUCT 4

            modelBuilder.Entity<Products>().HasData(new Products
            {
                Id = 32,
                Name = "Bồn cầu một khối TOTO MS904E4",
                UnitInStock = 200,
                UnitPrice = 19037000,
                Image = "https://admin.mingstores.com/core/public/themes/mingstores/products/UYZ61ie7Z7i5Hmjd6D7XyUWhBZVL7y8v.jpg",
                SoldQuantities = 1500,
                Description = "Thiết kế nguyên khối sang trọng, hiện đại\r\nNắp bàn cầu đóng êm, kèm vòi rửa nước lạnh Eco-washer\r\nBề mặt nước rộng giúp ngăn mùi hiệu quả\r\nThiết kế thân kín, vành kín tiện dụng cho việc vệ sinh hàng ngày\r\nCông nghệ CeFiONtect giúp lòng bàn cầu siêu nhẵn, hạn chế tối đa các vết bẩn, vi khuẩn\r\nCông nghệ xả G-Max êm, mạnh mẽ hiệu quả",
                MaterialStoreID = 1,
                Unit = "Bộ",
                Brand = "TOTO",
                Status = true
            });

            modelBuilder.Entity<ProductCategories>().HasData(new ProductCategories
            {
                CategoriesID = 1,
                ProductID = 32,
                Name = "Mỹ",

            });

            modelBuilder.Entity<ProductCategories>().HasData(new ProductCategories
            {
                CategoriesID = 2,
                ProductID = 32,
                Name = "Cái",
            });

            modelBuilder.Entity<ProductCategories>().HasData(new ProductCategories
            {
                CategoriesID = 4,
                ProductID = 32,
                Name = "Nhà vệ sinh",
            });
            modelBuilder.Entity<ProductCategories>().HasData(new ProductCategories
            {
                CategoriesID = 3,
                ProductID = 32,
                Name = "Hiện đại",
            });

            //PRODUCT 5

            modelBuilder.Entity<Products>().HasData(new Products
            {
                Id = 33,
                Name = "Bồn Cầu Thông Minh INAX AC-1017R/CW-KA22AVN",
                UnitInStock = 200,
                UnitPrice = 26301000,
                Image = "https://admin.mingstores.com/core/public/themes/mingstores/products/ryBdemssBcpSt6vbeQdirRUMcBszbZKt.jpg",
                SoldQuantities = 1500,
                Description = "Dòng sản phẩm bồn cầu INAX AC-1017R 1 khối cao cấp đến từ thương hiệu thiết bị vệ sinh INAX\r\nBệt Inax AC-1017R CW-KA22AVN 1 khối với thiết kế mới đơn giản, gọn gàng và sang trọng hơn kết hợp với những tính năng cải tiến\r\nCông nghệ ECO-X xã xoáy cuốn trôi mọi vết bẩn\r\nCông nghệ Aqua Ceramic giúp bề mạt men sứ trắng sáng trong suốt thời gian sữ dụng\r\nCông nghệ chống khuẩn HYPERKILAMIC kháng khuẩn độc quyền của INAX Nhật Bản. \r\nE-Clean: Chức năng phun rửa  tự động\r\nEvaClean: Chức năng vệ sinh phụ nữ\r\nCozyCare: Chức năng sưởi ấm bệ ngồi\r\nX-Fresh: Chức năng khử mùi nhanh \r\nEcoPower: Chức năng tiết kiệm điện “1 lần chạm” (8 tiếng sau tự khôi phục)\r\nDung tích két nước nóng: 0.67L (lít) Vòi phun rửa:\r\nVòi phun rửa và vòi phun dùng riêng cho phụ nữ đều là loại trượt tự động.  \r\nThiết bị an toàn: Rơ-le nhiệt, cảm ứng từ kiểm soát nhiệt độ cao, phao ngắt để thiết bị ngừng hoạt động khi không đủ nước, cảm ứng tự ngắt khi gặp sự cố. \r\nNước cấp: Nối trực tiếp từ đường ống nước ",
                MaterialStoreID = 1,
                Unit = "Bộ",
                Brand = "INAX",
                LastModifiedAt = DateTime.Parse("2022/5/22"),
                Status = true
            });

            modelBuilder.Entity<ProductCategories>().HasData(new ProductCategories
            {
                CategoriesID = 1,
                ProductID = 33,
                Name = "Mỹ",

            });

            modelBuilder.Entity<ProductCategories>().HasData(new ProductCategories
            {
                CategoriesID = 2,
                ProductID = 33,
                Name = "Cái",
            });

            modelBuilder.Entity<ProductCategories>().HasData(new ProductCategories
            {
                CategoriesID = 4,
                ProductID = 33,
                Name = "Nhà vệ sinh",
            });
            modelBuilder.Entity<ProductCategories>().HasData(new ProductCategories
            {
                CategoriesID = 3,
                ProductID = 33,
                Name = "Cổ điển",
            });

            //PRODUCT 6

            modelBuilder.Entity<Products>().HasData(new Products
            {
                Id = 34,
                Name = "GFS Gạch Granite Fusion Bán Bóng/Sần",
                UnitInStock = 200,
                UnitPrice = 520000,
                Image = "https://admin.mingstores.com/core/public/themes/mingstores/products/U3SmJsX6rBhkAPyQ3Xym2wlyoNTH6pGz.jpg",
                SoldQuantities = 1500,
                Description = "Màu sắc: vàng, xám, trắng, đen\r\nKích thước: 30x60, 60x60",
                MaterialStoreID = 1,
                Unit = "Cái",
                Brand = "NIRO GRANITE",
                LastModifiedAt = DateTime.Parse("2023/2/2"),
                Status = true
            });

            modelBuilder.Entity<ProductCategories>().HasData(new ProductCategories
            {
                CategoriesID = 1,
                ProductID = 34,
                Name = "Niro – Indonesia",

            });

            modelBuilder.Entity<ProductCategories>().HasData(new ProductCategories
            {
                CategoriesID = 2,
                ProductID = 34,
                Name = "Cái",
            });

            modelBuilder.Entity<Color>().HasData(new Color
            {
                Id = 6,
                Name = "Màu vàng",
            });

            modelBuilder.Entity<Color>().HasData(new Color
            {
                Name = "Màu xám",
                Id = 7,
            });

            modelBuilder.Entity<ProductSize>().HasData(new ProductSize
            {
                Name = "5M",
                Id = 4,
            });

            modelBuilder.Entity<ProductSize>().HasData(new ProductSize
            {
                Name = "10M",
                Id = 5,
            });


            modelBuilder.Entity<ProductType>().HasData(new ProductType
            {
                Id = 50,
                ColorId = 6,
                SizeID = 4,
                OtherID = 1,
                ProductID = 34,
                Quantity = 5,
                Status = Enum.Status.SUCCESS
            });

            modelBuilder.Entity<ProductType>().HasData(new ProductType
            {
                Id = 51,
                ColorId = 6,
                SizeID = 5,
                OtherID = 1,
                ProductID = 34,
                Quantity = 3,
                Status = Enum.Status.SUCCESS
            });

            modelBuilder.Entity<ProductType>().HasData(new ProductType
            {
                Id = 52,
                ColorId = 7,
                SizeID = 4,
                OtherID = 1,
                ProductID = 34,
                Quantity = 5,
                Status = Enum.Status.SUCCESS
            });

            modelBuilder.Entity<ProductType>().HasData(new ProductType
            {
                Id = 53,
                ColorId = 7,
                SizeID = 5,
                OtherID = 1,
                ProductID = 34,
                Quantity = 5,
                Status = Enum.Status.SUCCESS
            });
            //PRODUCT 7

            modelBuilder.Entity<Products>().HasData(new Products
            {
                Id = 35,
                Name = "Tủ Lavabo JM843",
                UnitInStock = 200,
                UnitPrice = 9520000,
                Image = "https://admin.mingstores.com/core/public/themes/mingstores/products/P5GERhsHMvYboHFoSTcetoIuHKJJApvD.jpg",
                SoldQuantities = 1500,
                Description = "ông nghệ sản xuất tủ lavabo của chúng tôi đã được chuyên nghiệp hóa qua nhiều năm phát triển, với phần khung bên ngoài được làm bằng nhôm, là cấu trúc chính hỗ trợ, giúp toàn bộ tủ chắc chắn, bên cạnh phần bản lề được làm bằng INOX 304 dày, giúp cho việc vận hành được trơn tru, ổn định.\r\n- Các bộ phận chính đều được làm bằng thép không gỉ 304 (INOX 304), tăng độ bền cho sản phẩm trong quá trình sử dụng.\r\n- Cấu hình cạnh và tay nắm cửa được làm bằng máy vát 45 độ đặc biệt, góc nhôm được gắn chặt vào thành bên trong tủ, để bề mặt sản phẩm mịn & tinh tế, tạo sự thoải mái khi sử dụng.\r\n\r\n- Việc sử dụng nhôm để làm vật liệu chính sản xuất tủ Lavabo là lựa chọn tối ưu nhất hiện nay, không chỉ có độ bền cao, nhôm hoàn toàn không độc hại với môi trường cũng như người sử dụng. Một số ưu điểm chính của nhôm:\r\n  + Trọng lượng nhẹ, độ bền cao, khả năng chịu lực lớn.\r\n  + Độ cứng tốt, không dễ biến dạng.\r\n  + Không thấm nước trong môi trường có độ ẩm cao, không bắt lửa và chịu được tác động mạnh.\r\n  + Lớp sơn phủ bền màu, chống ăn mòn do thời tiết hoặc hóa chất thông thường.\r\n  + Tạo không gian sang trọng, thoải mái và tiện lợi.",
                MaterialStoreID = 1,
                Unit = "Bộ",
                Brand = "JINMEI",
                LastModifiedAt = DateTime.Now,
                Status = true
            });

            modelBuilder.Entity<ProductCategories>().HasData(new ProductCategories
            {
                CategoriesID = 1,
                ProductID = 35,
                Name = "Niro – Indonesia",

            });

            modelBuilder.Entity<ProductCategories>().HasData(new ProductCategories
            {
                CategoriesID = 2,
                ProductID = 35,
                Name = "Bộ",
            });
            modelBuilder.Entity<ProductCategories>().HasData(new ProductCategories
            {
                CategoriesID = 4,
                ProductID = 35,
                Name = "Nhà tắm",
            });

            //PRODUCT 8

            modelBuilder.Entity<Products>().HasData(new Products
            {
                Id = 36,
                Name = "K015 Sơn Kansai chống thấm Water Proof 4L, 17L",
                UnitInStock = 200,
                UnitPrice = 750000,
                Image = "https://admin.mingstores.com/core/public/themes/mingstores/products/JnLYt6lx4OLgmoplQoxTPU1e9SBjZf9a.jpg",
                SoldQuantities = 1500,
                Description = "Sơn ngoại thất câo cấp đến từ thương hiệu Kansai nổi tiếng",
                MaterialStoreID = 1,
                Unit = "Lít",
                Brand = "KANSAI PAINT",
                LastModifiedAt = DateTime.Now,
                Status = true

            });

            modelBuilder.Entity<ProductCategories>().HasData(new ProductCategories
            {
                CategoriesID = 1,
                ProductID = 36,
                Name = "Ý",

            });

            modelBuilder.Entity<ProductCategories>().HasData(new ProductCategories
            {
                CategoriesID = 2,
                ProductID = 36,
                Name = "Bộ",
            });
            modelBuilder.Entity<ProductCategories>().HasData(new ProductCategories
            {
                CategoriesID = 4,
                ProductID = 36,
                Name = "Sơn nội thất",
            });

            //PRODUCT 9

            modelBuilder.Entity<Products>().HasData(new Products
            {
                Id = 37,
                Name = "KARI SQUARE STEP LIGHT (3W)",
                UnitInStock = 200,
                UnitPrice = 460000,
                Image = "https://admin.mingstores.com/core/public/themes/mingstores/products/N23c1P48S9v157cAYlNRc35gS92VeYVE.jpg",
                SoldQuantities = 1500,
                Description = "KARI SQUARE STEP LIGHT (3W) mang đến ánh sáng tinh tế đến gia đình bạn ",
                MaterialStoreID = 1,
                Unit = "Cái",
                Brand = "COSMOS",
                LastModifiedAt = DateTime.Parse("2023/10/1"),
                Status = true
            });

            modelBuilder.Entity<ProductCategories>().HasData(new ProductCategories
            {
                CategoriesID = 1,
                ProductID = 37,
                Name = "Ý",

            });

            modelBuilder.Entity<ProductCategories>().HasData(new ProductCategories
            {
                CategoriesID = 2,
                ProductID = 37,
                Name = "Bộ",
            });
            modelBuilder.Entity<ProductCategories>().HasData(new ProductCategories
            {
                CategoriesID = 4,
                ProductID = 37,
                Name = "Phòng khách",
            });

            //PRODUCT 10

            modelBuilder.Entity<Products>().HasData(new Products
            {
                Id = 38,
                Name = "GHR Gạch Granite Hardrock Mờ/Bán bóng",
                UnitInStock = 200,
                UnitPrice = 360000,
                Image = "https://admin.mingstores.com/core/public/themes/mingstores/products/6TauBDiJiwnvQaJTuCl9D0SYHFayTRHk.jpg",
                SoldQuantities = 1500,
                Description = "GHR Gạch Granite Hardrock Mờ/Bán bóng ",
                MaterialStoreID = 1,
                Unit = "Viên",
                Brand = "NIRO GRANITE",
                LastModifiedAt = DateTime.Now,
                Status = true
            });

            modelBuilder.Entity<ProductCategories>().HasData(new ProductCategories
            {
                CategoriesID = 1,
                ProductID = 38,
                Name = "Niro – Indonesia",

            });

            modelBuilder.Entity<ProductCategories>().HasData(new ProductCategories
            {
                CategoriesID = 2,
                ProductID = 38,
                Name = "Cái",
            });
            modelBuilder.Entity<ProductCategories>().HasData(new ProductCategories
            {
                CategoriesID = 4,
                ProductID = 38,
                Name = "Nội thất",
            });









            modelBuilder.Entity<Products>().HasData(new Products
            {
                Id = 1,
                Name = "Ngói lợp",
                UnitInStock = 300000,
                UnitPrice = 700,
                Unit = "ton",
                Image = "https://sbo.vn/wp-content/uploads/2021/06/tam-lop-sinh-thai-onduline.jpg",
                SoldQuantities = 3000,
                Description = "Ngói lợp kiểu Pháp cổ điển",
                MaterialStoreID = 1,
                Brand = "Pháp",
                Status = true
            });
            modelBuilder.Entity<Products>().HasData(new Products
            {
                Id = 2,
                Name = "Gạch lỗ",
                UnitInStock = 300000,
                UnitPrice = 40000,
                Unit = "ton",
                Image = "http://www.phudien.vn/upload/Product%20400x200/G%E1%BA%A1ch%20tuynel%20-%20g%E1%BA%A1ch%206%20l%E1%BB%97%20lo%E1%BA%A1i%20nh%E1%BB%8F.png",
                SoldQuantities = 1500,
                Description = "Gạch 2 lỗ cao cấp đến từ thương hiệu nổi tiếng ",
                MaterialStoreID = 1,
                Brand = "Việt Nam",
                Status = true
            });
            modelBuilder.Entity<Products>().HasData(new Products
            {
                Id = 3,
                Name = "Sơn chống thấm",
                UnitInStock = 2000,
                Unit = "ton",
                UnitPrice = 1000000,
                Image = "https://nipponpaint.com.vn/sites/default/files/inline-images/son-chong-tham-la-gi-1.jpg",
                SoldQuantities = 1500,
                Description = "Sơn chống thấm Nippon",
                MaterialStoreID = 1,
                Brand = "Mỹ",
                Status = true
            });
            modelBuilder.Entity<Products>().HasData(new Products
            {
                Id = 4,
                Name = "Cát Mịn",
                UnitInStock = 200000,
                UnitPrice = 50000,
                Unit = "ton",
                Image = "https://sbshouse.vn/wp-content/uploads/2020/09/cat-xay-dung.jpg",
                SoldQuantities = 50,
                Description = "Cát mịn dành cho xây dựng đặc biệt dành cho ngôi nhà yêu dấu của bạn",
                MaterialStoreID = 1,
                Brand = "Việt Nam",
                Status = true
            });
            modelBuilder.Entity<Products>().HasData(new Products
            {
                Id = 5,
                Name = "Giường ngủ công chúa",
                UnitInStock = 3,
                Unit = "cái",
                UnitPrice = 30000000,
                Image = "https://vuongquocnoithat.vn/images/2016/09/22/phong-ngu-phong-cach-cong-chua-jy921.jpg",
                SoldQuantities = 300,
                Description = "Miễn phí vận chuyển, lắp đặp tại Hà Nội & HCM",
                MaterialStoreID = 2,
                Brand = "Koreana",
                Status = true
            });
            modelBuilder.Entity<ProductCategories>().HasData(new ProductCategories
            {
                CategoriesID = 2,
                ProductID = 5,
                Name = "Gỗ tự nhiên",
            });
            modelBuilder.Entity<ProductType>().HasData(new ProductType
            {
                Id = 63,
                SizeID = 14,
                ProductID = 5,
                Quantity = 1,
                Status = Enum.Status.SUCCESS
            });
            modelBuilder.Entity<ProductType>().HasData(new ProductType
            {
                Id = 64,
                SizeID = 15,
                ProductID = 5,
                Quantity = 2,
                Status = Enum.Status.SUCCESS
            });
            modelBuilder.Entity<ProductSize>().HasData(new ProductSize
            {
                Id = 14,
                Name = "Giường 1,8m"
            });
            modelBuilder.Entity<ProductSize>().HasData(new ProductSize
            {
                Id = 15,
                Name = "Giường 2m"
            });
            modelBuilder.Entity<Products>().HasData(new Products
            {
                Id = 6,
                Name = "Đèn chùm đồng cổ điển phong cách Ý",
                UnitInStock = 3,
                Unit = "cái",
                UnitPrice = 2000000,
                Image = "https://vuongquocnoithat.vn/images/2018/01/29/den-chum-dong-co-dien-kieu-italia%20atl8501.jpg",
                SoldQuantities = 1,
                Description = "Nhập khẩu 100%\r\n\r\n -Bảo hành 2 năm\r\n\r\n -Miễn phí vận chuyển, lắp đặp tại Hà Nội & HCMShowroom HN: 3000m2 Tầng 1&2, tòa T2",
                MaterialStoreID = 2,
                Brand = "AUSTRIAN LAMP",
                Status = true
            });
            modelBuilder.Entity<ProductCategories>().HasData(new ProductCategories
            {
                CategoriesID = 2,
                ProductID = 6,
                Name = "Đồng",

            });
            modelBuilder.Entity<ProductType>().HasData(new ProductType
            {
                Id = 60,
                SizeID = 10,
                ProductID = 6,
                Quantity = 3,
                Status = Enum.Status.SUCCESS
            });
            modelBuilder.Entity<ProductType>().HasData(new ProductType
            {
                Id = 61,
                SizeID = 11,
                ProductID = 6,
                Quantity = 3,
                Status = Enum.Status.SUCCESS
            });
            modelBuilder.Entity<ProductSize>().HasData(new ProductSize
            {
                Id = 10,
                Name = "6 bóng"
            });
            modelBuilder.Entity<ProductSize>().HasData(new ProductSize
            {
                Id = 11,
                Name = "8 bóng"
            });


            modelBuilder.Entity<Products>().HasData(new Products
            {
                Id = 20,
                Name = "K023 Sơn Kansai chống thấm Aqua Shield 5L, 18L",
                UnitPrice = 960000,
                UnitInStock = 1000,
                Image = "https://admin.mingstores.com/core/public/themes/mingstores/products/OOUUL3p3xO6kV63bOCyr4qCMZBNDo2yc.jpg",
                Description = "- Sơn chống thấm Một thành phần Aqua Shield\r\n\r\n- Chống thấm tuyệt hảo\r\n\r\n- Kháng nước tuyệt đối\r\n\r\n- Che phủ vết nứt, co giãn tốt, dễ thi công (không chứa xi măng)\r\n\r\n",
                Brand = "KANSAI PAINT",
                SoldQuantities = 100,
                Unit = "Lít",
                MaterialStoreID = 1,
                Status = true
            });

            modelBuilder.Entity<ProductCategories>().HasData(new ProductCategories
            {
                CategoriesID = 1,
                ProductID = 20,
                Name = "Niro – Indonesia",

            });

            modelBuilder.Entity<ProductCategories>().HasData(new ProductCategories
            {
                CategoriesID = 2,
                ProductID = 20,
                Name = "Lít",
            });
            modelBuilder.Entity<ProductCategories>().HasData(new ProductCategories
            {
                CategoriesID = 4,
                ProductID = 20,
                Name = "Sơn ngoại thất",
            });

            modelBuilder.Entity<ProductSize>().HasData(new ProductSize
            {
                Id = 7,
                Name = "5L",

            });

            modelBuilder.Entity<ProductSize>().HasData(new ProductSize
            {
                Id = 8,
                Name = "18L",

            });

            //modelBuilder.Entity<Other>().HasData(new Other
            //{
            //    Id = 7,
            //    Name = "Ngoại thất",

            //});

            //modelBuilder.Entity<Other>().HasData(new Other
            //{
            //    Id = 8,
            //    Name = "Nội thất",

            //});

            modelBuilder.Entity<ProductType>().HasData(new ProductType
            {
                Id = 65,
                SizeID = 7,
                ColorId = 1,
                OtherID = 1,
                ProductID = 20,
                Quantity = 5,
                Status = Enum.Status.SUCCESS

            });
            modelBuilder.Entity<ProductType>().HasData(new ProductType
            {
                Id = 66,
                SizeID = 8,
                ColorId = 1,
                OtherID = 1,
                ProductID = 20,
                Quantity = 3,
                Status = Enum.Status.SUCCESS

            });





            modelBuilder.Entity<Products>().HasData(new Products
            {
                Id = 21,
                Name = "SEN TẮM CÂY INAX BFV-515S",
                UnitPrice = 12485000,
                UnitInStock = 1200,
                Image = "https://admin.mingstores.com/core/public/themes/mingstores/products/Mjzhtin7lD3gCUXksET0srIdUnABPNE3.jpg",
                Description = "Sen cây nóng lạnh INAX BFV-515S là sản phẩm sen cây INAX  được thiết kế tay sen cài liền cùng thân sen cây thay vì để gắn tường, giúp cho tổng thể bộ sen cây trở nên gọn gàng, linh hoạt, đặc biệt phù hợp cả với những căn phòng tắm kích thước nhỏ, quý khách hàng vẫn có thể lắp đặt mẫu sen cây này và cảm nhận trải nghiệm khác biệt khi tắm vòi sen cây với bát sen lớn.\r\nMẫu thiết kế sen cây thuộc dòng sản phẩm SEN VÒI INAX có thiết kế đẹp mắt, sáng tạo từ kiểu dáng đến tính năng thích hợp cho mọi loại hình phòng tắm từ những phòng tắm đơn giản, nhỏ hẹp, đến những căn phòng tắm hiện đại, tiện nghi. ",
                Brand = "INAX",
                Unit = "Cái",
                SoldQuantities = 1000,
                MaterialStoreID = 1,
                Status = true
            });

            modelBuilder.Entity<ProductCategories>().HasData(new ProductCategories
            {
                CategoriesID = 1,
                ProductID = 21,
                Name = "Nhật Bản",

            });

            modelBuilder.Entity<ProductCategories>().HasData(new ProductCategories
            {
                CategoriesID = 2,
                ProductID = 21,
                Name = "Cái",
            });
            modelBuilder.Entity<ProductCategories>().HasData(new ProductCategories
            {
                CategoriesID = 4,
                ProductID = 21,
                Name = "Nhà tắm",
            });

            modelBuilder.Entity<Products>().HasData(new Products
            {

                Id = 22,
                Name = "Chậu Rửa Lavabo Inax AL-536V",
                UnitPrice = 2046000,
                UnitInStock = 100,
                Image = "https://admin.mingstores.com/core/public/themes/mingstores/products/JnyguIQW8EMvvUqcZ6BZnGSLOeL5OgpK.jpg",
                Description = "Là mẫu chậu rửa mặt Inax đặt bàn mới nhất 2017, sản phẩm tiêu biểu cho năm 2018",
                Brand = "INAX",
                Unit = "Cái",
                SoldQuantities = 100,
                MaterialStoreID = 1,
                Status = true
            });

            modelBuilder.Entity<ProductCategories>().HasData(new ProductCategories
            {
                CategoriesID = 1,
                ProductID = 22,
                Name = "Nhật Bản",

            });

            modelBuilder.Entity<ProductCategories>().HasData(new ProductCategories
            {
                CategoriesID = 2,
                ProductID = 22,
                Name = "Cái",
            });
            modelBuilder.Entity<ProductCategories>().HasData(new ProductCategories
            {
                CategoriesID = 4,
                ProductID = 22,
                Name = "Nhà tắm",
            });
            #endregion

            #region Notification
            modelBuilder.Entity<Notification>().HasData(new Notification
            {
                Id = 1,
                Type = Enum.NotificationType.CONTRACTOR_POST_NOTIFICATION,
                Title = "New Notification",
                UserID = Guid.Parse("d7285fb7-835b-4680-a18c-673bd71f63d7"),
                CreateBy = Guid.Parse("d7285fb7-835b-4680-a18c-673bd71f63d9"),
                IsRead = false,
                LastModifiedAt = DateTime.Now,
                Message = "Someone has saved your post",
                NavigateId = 1
            });

            modelBuilder.Entity<Notification>().HasData(new Notification

            {
                Id = 2,
                Type = Enum.NotificationType.CONTRACTOR_POST_NOTIFICATION,
                Title = "New Notification",
                UserID = Guid.Parse("d7285fb7-835b-4680-a18c-673bd71f63d7"),
                CreateBy = Guid.Parse("d7285fb7-835b-4680-a18c-673bd71f63d9"),
                IsRead = false,
                LastModifiedAt = DateTime.Now,
                Message = "Someone has applied your post",
                NavigateId = 1
            });

            modelBuilder.Entity<Notification>().HasData(new Notification
            {
                Id = 3,
                Type = Enum.NotificationType.CONTRACTOR_POST_NOTIFICATION,
                Title = "New Notification",
                UserID = Guid.Parse("d7285fb7-835b-4680-a18c-673bd71f63d7"),
                CreateBy = Guid.Parse("d7285fb7-835b-4680-a18c-673bd71f63d9"),
                IsRead = false,
                LastModifiedAt = DateTime.Now,
                Message = "Create commitment successfully",
                NavigateId = 1
            });
            #endregion


            #region Product Type Default Value
            modelBuilder.Entity<Color>().HasData(new Color
            {
                Id = 1,
                Name = "No Color"
            });

            modelBuilder.Entity<ProductSize>().HasData(new ProductSize
            {
                Id = 1,
                Name = "No Size"
            });

            modelBuilder.Entity<Other>().HasData(new Other
            {
                Id = 1,
                Name = "No Other"
            });
            #endregion

            #region Applied Post
            modelBuilder.Entity<AppliedPost>().HasData(new AppliedPost
            {
                BuilderID = 1,
                PostID = 3,
                AppliedDate = DateTime.Now,
                Status = Enum.Status.ACCEPTED,

            });

            modelBuilder.Entity<AppliedPost>().HasData(new AppliedPost
            {
                BuilderID = 2,
                PostID = 4,
                AppliedDate = DateTime.Now,
                Status = Enum.Status.ACCEPTED,

            });

            modelBuilder.Entity<AppliedPost>().HasData(new AppliedPost
            {
                BuilderID = 3,
                PostID = 4,
                AppliedDate = DateTime.Now,
                Status = Enum.Status.ACCEPTED,

            });

            modelBuilder.Entity<AppliedPost>().HasData(new AppliedPost
            {
                BuilderID = 4,
                PostID = 1,
                AppliedDate = DateTime.Now,
                Status = Enum.Status.ACCEPTED,
                QuizId = 1

            });
            #endregion

            #region Quiz 1 
            modelBuilder.Entity<Quiz>().HasData(new Quiz
            {
                Id = 1,
                Name = "Bài test thợ xây ",
                PostID = 1,
                TypeID = Guid.Parse("4ace8fcb-95eb-48c0-9deb-240e8b4e10e0"),
                LastModifiedAt = DateTime.Now

            });



            modelBuilder.Entity<Question>().HasData(new Question
            {
                Id = 1,
                Name = "Búa tạ, cáo búa  đều là những loại gì?",
                QuizId = 1,
            }); ;

            modelBuilder.Entity<Answer>().HasData(new Answer
            {
                Id = 1,
                Name = "Máy khoan",
                isCorrect = false,
                QuestionId = 1

            });

            modelBuilder.Entity<Answer>().HasData(new Answer
            {
                Id = 2,
                Name = "Búa",
                isCorrect = true,
                QuestionId = 1

            });

            modelBuilder.Entity<Answer>().HasData(new Answer
            {
                Id = 3,
                Name = "Cưa",
                isCorrect = false,
                QuestionId = 1

            });

            modelBuilder.Entity<Answer>().HasData(new Answer
            {
                Id = 4,
                Name = "Vặn vít",
                isCorrect = false,
                QuestionId = 1

            });


            //QUESTION 2


            modelBuilder.Entity<Question>().HasData(new Question
            {
                Id = 2,
                Name = "Quá trình phá hủy một tòa nhà được gọi là _______ ?",
                QuizId = 1,
            }); ;

            modelBuilder.Entity<Answer>().HasData(new Answer
            {
                Id = 5,
                Name = "Phá dỡ",
                isCorrect = true,
                QuestionId = 2

            });

            modelBuilder.Entity<Answer>().HasData(new Answer
            {
                Id = 6,
                Name = "Ràng buộc ",
                isCorrect = false,
                QuestionId = 2

            });

            modelBuilder.Entity<Answer>().HasData(new Answer
            {
                Id = 7,
                Name = "Cốt thép",
                isCorrect = false,
                QuestionId = 2

            });

            modelBuilder.Entity<Answer>().HasData(new Answer
            {
                Id = 8,
                Name = "Đùn",
                isCorrect = false,
                QuestionId = 2

            });

            //QUESTION 3

            modelBuilder.Entity<Question>().HasData(new Question
            {
                Id = 3,
                Name = "Khi một ngôi nhà sử dụng năng lượng mặt trời, bạn thường sẽ thấy ________ trên mái nhà.?",
                QuizId = 1,
            }); ;

            modelBuilder.Entity<Answer>().HasData(new Answer
            {
                Id = 9,
                Name = "Ăng-ten",
                isCorrect = false,
                QuestionId = 3

            });

            modelBuilder.Entity<Answer>().HasData(new Answer
            {
                Id = 10,
                Name = "Giếng trời ",
                isCorrect = false,
                QuestionId = 3

            });

            modelBuilder.Entity<Answer>().HasData(new Answer
            {
                Id = 11,
                Name = "Tấm",
                isCorrect = true,
                QuestionId = 3

            });

            modelBuilder.Entity<Answer>().HasData(new Answer
            {
                Id = 12,
                Name = "Ống",
                isCorrect = false,
                QuestionId = 3

            });

            //QUESTION 4

            modelBuilder.Entity<Question>().HasData(new Question
            {
                Id = 4,
                Name = "Tên khác của một bức tường không chịu lực là gì?",
                QuizId = 1,
            }); ;

            modelBuilder.Entity<Answer>().HasData(new Answer
            {
                Id = 13,
                Name = "Bươm bướm",
                isCorrect = false,
                QuestionId = 4

            });

            modelBuilder.Entity<Answer>().HasData(new Answer
            {
                Id = 14,
                Name = "Màn ",
                isCorrect = false,
                QuestionId = 4

            });

            modelBuilder.Entity<Answer>().HasData(new Answer
            {
                Id = 15,
                Name = "Đảng",
                isCorrect = true,
                QuestionId = 4

            });

            modelBuilder.Entity<Answer>().HasData(new Answer
            {
                Id = 16,
                Name = "Chỉ",
                isCorrect = false,
                QuestionId = 4

            });




            #endregion

            #region Quiz 2
            modelBuilder.Entity<Quiz>().HasData(new Quiz
            {
                Id = 2,
                Name = "Bài test thợ sơn ",
                PostID = 1,
                TypeID = Guid.Parse("ce9fa65b-d005-46b6-953e-e6462a59cfb3"),
                LastModifiedAt = DateTime.Now,

            });
            #endregion

        }
    }
}
