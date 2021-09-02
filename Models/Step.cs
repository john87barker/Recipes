namespace Recipes.Models
{
    public class Step
    {
        public string Body { get; set; }
        public int RecipeId { get; set; }
        public string CreatorId{ get; set; }
        public Account Creator { get; set; }
        public int Id { get; set; }
  }
}