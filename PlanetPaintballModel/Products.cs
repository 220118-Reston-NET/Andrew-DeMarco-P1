namespace PPModel
{

    public class Products
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }

        public int quantity { get; set; }

        public override string ToString()
        {   
            return $"======================\nID: {ID}\nName: {Name}\nPrice: ${Price.ToString("0.00")}\nDescription: {Description}\nCategory: {Category}\nQuantity: {quantity}";    
        }

    }

    
}