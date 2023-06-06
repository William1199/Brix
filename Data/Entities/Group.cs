using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Group
    {
        public int Id { get; set; }

        public Builder Builder { get; set; }

        public int BuilderID { get; set; }

        public int PostID { get; set; }

        public List<GroupMember> GroupMembers { get; set;}

        public List<AppliedPost> AppliedPosts { get; set;}

        public List<PostCommitment>? PostCommitments { get; set; }
    }
}
