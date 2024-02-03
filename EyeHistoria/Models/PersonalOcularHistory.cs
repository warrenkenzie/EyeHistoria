namespace EyeHistoria.Models
{
    public class PersonalOcularHistory
    {
        public int p_id { get; set; }
        public string p_prescription { get; set; }
        public string p_procedure { get; set; }
        public string p_condition { get; set; }

        public string? pre_type { get; set; }
        public string? pro_type { get; set; }
        public string? con_type { get; set; }

        public DateTime? pre_sdate { get; set; }
        public DateTime? pro_sdate { get; set; }
        public DateTime? con_sdate { get; set; }

        public DateTime? pre_edate { get; set; }
        public DateTime? pro_edate { get; set; }
        public DateTime? con_edate { get; set; }
    }
}
