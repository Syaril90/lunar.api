using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectLunar.API.Contracts.Response
{
    public class PrayerPlaceResponseDetailsContract
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Remarks { get; set; }
        public string Category { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public int? LikeCount { get; set; }
        public int? DislikeCount { get; set; }
        public DateTime? InsAt { get; set; }
        public string InsBy { get; set; }
        public DateTime? UpdAt { get; set; }
        public string UpdBy { get; set; }
        public UserActionContract UserAction { get; set; }
        public List<PhotoResponseContract> Photos { get; set; }
    }

    public class PhotoResponseContract
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public byte[] File { get; set; }
        public string Extension { get; set; }
        public DateTime? InsAt { get; set; }
        public string InsBy { get; set; }
        public DateTime? UpdAt { get; set; }
        public string UpdBy { get; set; }
    }

    public class UserActionContract
    {
        public Guid Id { get; set; }
        public bool? isLike { get; set; }
        public bool? isDislike { get; set; }
        public DateTime? InsAt { get; set; }
        public string InsBy { get; set; }
        public DateTime? UpdAt { get; set; }
        public string UpdBy { get; set; }
    }

}
