using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Models
{
    [SerializeField]
    public class TeamMembersModel
    {
        public string Title;
        public List<string> ColumnHeaders;
        public List<TeamMemberModel> Data;
    }
}

