namespace ConsoleApp5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var context = new Context();

            var User = new User(context);

            var products = await userService.GetAvailableProductsAsync();
            foreach (var p in products)
            {
                Console.WriteLine($"{p.Id}, {p.Name}, {p.Price}, {p.Quantity}");
            }

            try
            {
                await userService.PlaceOrderAsync(productId: 1, quantity: 2);
                Console.WriteLine("successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Order failed: {ex.Message}");
            }

            var updatedProduct = await context.Products.FindAsync(1);
            Console.WriteLine(updatedProduct.Quantity);
        }
    }
}
