namespace Recipes.Models
{
    public class Recipe
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int CookTime { get; set; }
        public int PrepTime { get; set; }
        public string CreatorId{ get; set; }
        public Account Creator{ get; set; }
        public int Id{ get; set; }
  }
}