namespace Identity.Domain.Model
{
    public class Plan: Entity
    {
        public bool CancelAnytime { get; set; }
        public bool HD { get; set; }
        public string Name { get; set; }
        public int NoScreens { get; set; }
        public bool UltraHD { get; set; }
        public double MonthlyPrice { get; set; }
    }
}
