using System;
using System.Collections.Generic;
using System.Linq;


namespace ExamenSegundaSemana
{
    class Program
    {


        static void Main(string[] args)
        {

            
             var p1 = new Pizza() { Id =  Guid.NewGuid(), Name = "Carbonara" };
            var p2 = new Pizza() { Id = Guid.NewGuid(), Name = "Barbacoa" };
            var p3 = new Pizza() { Id = Guid.NewGuid(), Name = "Mediterranea" };
            var p4 = new Pizza() { Id = Guid.NewGuid(), Name = "Tropical" };
            List<Pizza> pizzas = new List<Pizza>() { p1,p2,p3,p4};


            var i1 = new Ingredient() { Id = Guid.NewGuid(), Name = "Queso", Cost= 3.50m };
            var i2 = new Ingredient() { Id = Guid.NewGuid(), Name = "Tomate", Cost= 3.00m };
            var i3 = new Ingredient() { Id = Guid.NewGuid(), Name = "Jamon", Cost= 2.50m };
            var i4 = new Ingredient() { Id = Guid.NewGuid(), Name = "Bacon", Cost= 3.50m };
            var i5 = new Ingredient() { Id = Guid.NewGuid(), Name = "Piña", Cost= 2.75m };
            List<Ingredient> ingredientes = new List<Ingredient>() { i1, i2,i3,i4,i5};


            var pi1 = new PizzaIngredient() { Id = Guid.NewGuid(), PizzaId=p1.Id, IngredientId = i1.Id};
            var pi2 = new PizzaIngredient() { Id = Guid.NewGuid(), PizzaId=p2.Id, IngredientId = i3.Id};
            var pi3 = new PizzaIngredient() { Id = Guid.NewGuid(), PizzaId=p2.Id, IngredientId = i3.Id};
            var pi4 = new PizzaIngredient() { Id = Guid.NewGuid(), PizzaId=p4.Id, IngredientId = i5.Id};
            List<PizzaIngredient> pizzasingredientes = new List<PizzaIngredient>() { pi1, pi2, pi3, pi4 };

            //ejercicio 1
            var query = from pizza in pizzas
                        join pizzaingrediente in pizzasingredientes on pizza.Id equals pizzaingrediente.PizzaId
                        join ingrediente in ingredientes on pizzaingrediente.IngredientId equals ingrediente.Id
                        group ingrediente by new { pizza.Id, pizza.Name, ingrediente.Cost}   into i
                        select  new { Id = i.Key.Id, Nombre= i.Key.Name, Precio = i.Sum(ingrediente => ingrediente.Cost)*1.20m};


            //ejercicio 2
            var query2 = from pizza in pizzas
                        join pizzaingrediente in pizzasingredientes on pizza.Id equals pizzaingrediente.PizzaId into ps
                        from pizzaingrediente in ps.DefaultIfEmpty() 
                        select new { Id = pizza.Id, Nombre = pizzaingrediente == null ? pizza.Name + "(Sin Ingredientes)" : pizza.Name };

            foreach (var pizza in query2) {

                Console.WriteLine(pizza.Id + " " + pizza.Nombre );
            }
            Console.ReadLine();
        }
    }


    //ejercicio 3
    public class Pizza3
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Ingredient> ingredientes;

        public decimal getPrice() {

            var precio = 0.0m;
            foreach (var ingrediente in ingredientes) {
                precio += ingrediente.Cost;
            }

            return precio*1.20m;
        }

        public List<Ingredient> getIngredientes() {
            return this.ingredientes;
        }
    }


    //ejercicio 4
    public interface mapper
    {
        bool create(object o);
        bool update(object o);
        bool delete(object o);
        object select(int id);
    }

    //modelo dado
    public class Pizza
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
    public class Ingredient
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Decimal Cost { get; set; }
    }
    public class PizzaIngredient
    {
        public Guid Id { get; set; }
        public Guid PizzaId { get; set; }
        public Guid IngredientId { get; set; }
    }



}
