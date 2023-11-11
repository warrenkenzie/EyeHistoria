namespace EyeHistoria.Models
{
    public class GeneralQuestion
    {
        public Question General_Question { get; set; }

        public List<Question> Follow_Questions { get; set; }
    }
}
