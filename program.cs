using System;
using System.Collections.Generic;
using OpenXmlPowerTools;
using PizzaBox.Domain.Models;
using PizzaBox.Storing.Repositories;


namespace PizzaBox.Client
{
    class Program
    {
        private static OrderRepos orderRepos = new OrderRepos();
        static StoreRepos storeRepos = new StoreRepos();
        private static CustomerRepos custRepos = new CustomerRepos();
        private static ManagerRepos managRepos = new ManagerRepos();

        static void Main()
        {
            Console.Clear();
            Start();
        }

        static void Exit()
        {
            Console.WriteLine("Now Exiting...");
        }

        static void Start()
        {
            Console.WriteLine("!!Welcome!!");
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Manager Login");
            Console.WriteLine("3. Register");
            Console.WriteLine("4. Exit");
            Console.Write("What would you like to do? ");

            int input = UserInput();

            switch (input)
            {
                case 1:
                    Login();
                    break;
                case 2:
                    ManagerLogin();
                    break;
                case 3:
                    Register();
                    break;
                case 4:
                    Exit();
                    break;
                default:
                    Start();
                    break;
            }
        }

        public static void Login()

        {
            Console.Clear();

            CustomerDetails CurrentUser = new CustomerDetails();

            Console.Write("Email:");
            CurrentUser.Email = Console.ReadLine();
            Console.Write("Password:");
            CurrentUser.Password = Console.ReadLine();
            if (custRepos.Read(CurrentUser) == null)
            { 
                Console.Clear();
                Console.WriteLine("No user with that email/passord combination exists\nTry again\n");
                Start();
                return;
            }

            Console.Clear();
            Console.WriteLine(".....Login SUCCESSFUL...\n");
            CDash(custRepos.Read(CurrentUser));

        }

        public static void ManagerLogin()

        {

            Console.Clear();

            ManagerDetails CurrentUser = new ManagerDetails();

            Console.Write("Manager Email:");
            string email = Console.ReadLine();
            Console.Write("Manager Password:");
            string pass = Console.ReadLine();

            CurrentUser = managRepos._managerList.Find(x => x.Email == email && x.Password == pass);

            if (CurrentUser == null)
            {
                Console.Clear();
                Console.WriteLine("There is not a manager with that email or passord :( ... \n Please Try again\n");
                Start();
                return;
            }

            CurrentUser.CurrentStore = storeRepos.Get(new StoreDetails("MizzaPizza"," Arlington, TX"));

            Console.Clear();
            Console.WriteLine("...Login SUCCESSFUL...\n");
            MDash(CurrentUser);
        }

        public static void Register()
        {
            Console.Clear();

            CustomerDetails cust = new CustomerDetails();
            
            Console.Write("Enter First Name: ");
            cust.FirstName = Console.ReadLine();
            Console.Write("Enter Last Name: ");
            cust.LastName = Console.ReadLine();
            Console.Write("Enter Email: ");
            cust.Email = Console.ReadLine();
            Console.Write("Password: ");
            cust.Password = Console.ReadLine();
            Console.Write("Address: ");
            cust.Address = Console.ReadLine();


            if (custRepos.Read(cust) != null)
            {
                Console.Clear();
                Console.WriteLine("This email already belomgs to a current customer\n Please Try again\n");
                
                Start();
            }

            custRepos.Create(cust);

            Console.WriteLine(" {0} has successfuly registered :) ", cust.FirstName);
            CDash(cust);

            // return;

        }

        public static void CDash(CustomerDetails cust)
        {
            Console.WriteLine("Welcome {0}, would you like to...", cust.FirstName);
            Console.WriteLine("1. START A NEW ORDER?");
            Console.WriteLine("2. VIEW YOU'RE ORDER HISTORY");
            Console.WriteLine("3. or LOGOUT");
            Console.Write("What would you like to do?");

            int input = UserInput();

            switch (input)
            {
                case 1:
                    StartOrder(cust);
                    break;
                case 2:
                    CustomerOrderHistory(cust);
                    break;
                case 3:
                    Console.WriteLine("Bye {0}", cust.FirstName);
                    Start();
                    break;
                default:
                    CDash(cust);
                    break;
            }
        }

        public static void MDash(ManagerDetails man)
        {
            Console.WriteLine("Welcome {0} from {1}", man.FirstName, man.CurrentStore);
            Console.WriteLine("1. View store orders");
            Console.WriteLine("2. Change current store");
            Console.WriteLine("3. Logout");
            Console.Write("What would you like to do:");
           
            int input = UserInput();

            switch (input)
            {
                case 1:
                    ViewStoreOrders(man);
                    break;
                case 2:
                    ChangeCurrentStore(man);
                    break;
                case 3:
                    Console.WriteLine("Okay, Good Bye {0}", man.FirstName);
                    Start();
                    break;
                default:
                    MDash(man);
                    break;
            }
        }

        public static void StartOrder(CustomerDetails cust)
        {
            

            if (AllowOrderTwoHrPd(cust))
            {
                CDash(cust);
                return;
            }
            StoreDetails S = new StoreDetails();

 
            OrderDetails O = new OrderDetails();

            S = ChooseStore();
          
            if (AllowOrderTwentyFourHrPd(S, cust))
            {
                CDash(cust);
                return;
            }


            Console.WriteLine("You chose to order from {0}", S.Name);


            O = CreateOrder();

            Console.Clear();
            Console.WriteLine("Your order has been placed successfully");
            


            O.StoreName = S.Name;
           

            
            O.CustId = cust.Id;

            // confirm order
            
            S.AddOrder(O);
            storeRepos.Update(S);
            cust.AddOrder(O);

            orderRepos.Create(O);
            custRepos.Update(cust);

            Console.Clear();
            Console.WriteLine("Your order has been placed successfully");

            O.PrintOrder();
            CDash(cust);
        }

        public static bool AllowOrderTwoHrPd(CustomerDetails cust)
        {
            if (cust.Orders.Count > 0)
            {
                TimeSpan now = DateTime.Now - cust.Orders[cust.Orders.Count - 1].OrderDate;

                if (now < new TimeSpan(0, 2, 0, 0))
                {
                    now = new TimeSpan(2, 0, 0) - now;
                    Console.WriteLine("......Error...\nMust wait {0} minutes to before ordering again", now);
                    return true;
                }
            }
            return false;
        }

        public static bool AllowOrderTwentyFourHrPd(StoreDetails S, CustomerDetails cust)
        {
            //OrderDetails O = cust.Orders.Find(O => O.StoreName == S.Name);
            //   OrderDetails O = cust.Orders.Find(O => { if (O == null) { return false; } else { return O.StoreName == S.Name; } });
            OrderDetails O = cust.Orders.Find(O => { if (O == null || O.StoreName == null) { return false; } else { return O.StoreName.Equals(S.Name); } });
            if (O != null)

            {
                TimeSpan now = DateTime.Now - O.OrderDate;

                if (now < new TimeSpan(0, 24, 0, 0) && S.Name == O.StoreName)
                {
                    now = new TimeSpan(24, 0, 0) - now;
                    Console.WriteLine(".....Error...\nMust wait {0} minutes to order from {1} again\n", now, S);
                    CDash(cust);

                    return true;
                }
            }
            return false;
        }

        public static void CustomerOrderHistory(CustomerDetails cust)
        {
            Console.Clear();
            cust.ViewOrderHistory();
            CDash(cust);
        }

        public static StoreDetails ChooseStore()
        {
            Console.Clear();
            Console.WriteLine("Stores: ");
            Console.WriteLine("1. MizzaPizza .  .  . Lubbock, TX");
            Console.WriteLine("2. RangerPizza .  .  . Arlington, TX");
            Console.WriteLine("3. CowboyPizza .  .  . Dallas, TX");
            Console.Write("Choose store: ");

            int input = UserInput();
            switch (input)
            {
                case 1:
                    var store1 = new StoreDetails("MizzaPizza", "Lubbock, TX");
                    return storeRepos.Get(store1);
                case 2:
                    return storeRepos.Get(new StoreDetails("RangerPizza", "Arlington, TX"));
                case 3:
                    return storeRepos.Get(new StoreDetails("CowboyPizza", "Dallas, TX"));
                default:
                    return ChooseStore();
                    
            }

        }

        public static OrderDetails CreateOrder()
        {
            Console.Write("How many pizzas do you want? ");

            int input = UserInput();
            
            if (input == -1 || input == 0 || input > 100)
            {
                if (input > 100)
                    Console.WriteLine("Yikes...You can't order more than 100 pizzas");
                return null;
            }

            List<PizzaDetails> pList = new List<PizzaDetails>();

            for (int i = 0; i < input; i++)
            {
                PizzaDetails newPizza = CreatePizza();
                pList.Add(newPizza);
                Console.WriteLine("Your pizza is a {0}", newPizza);
            }

            OrderDetails O = new OrderDetails(pList);
            if (O.OrderTotal >= 250.00M)
            {
                Console.Clear();
                Console.WriteLine("Order total can't be more than $250");

                return null;
            }

            return O;

        }

        static PizzaDetails CreatePizza()
        {
            Console.WriteLine("1. Large Pepperoni Pizza           $9.99");
            Console.WriteLine("2. Large Cheese Pizza              $7.99");
            Console.WriteLine("3. Upside-Down Pizza                  $250.00");
            Console.WriteLine("4. Create your own pizza           $4.99-$9.99");
            Console.Write("Please select a specialty pizza: ");

            int input = UserInput();

            switch (input)
            {
                case 1:
                    return new PizzaDetails("Large", "Thick", "Pepperoni");
                case 2:
                    return new PizzaDetails("Large", "Hand-tossed", "Cheese");
                case 3:
                    return new PizzaDetails("test", "Hand-tossed", "Pepperoni");
                case 4:
                    return CustomizePizza();
                default:
                    return CreatePizza();
            }

        }

        static PizzaDetails CustomizePizza()
        {
            PizzaDetails newPizza = new PizzaDetails();
            newPizza.Size = ChooseSize();
            newPizza.Crust = ChooseCrust();
            newPizza.Topping = ChooseTopping();
            return newPizza;
        }

        static string ChooseSize()
        {
            Console.WriteLine("SIZESSsssssssss: ");
            Console.WriteLine("1. Small     $4.99");
            Console.WriteLine("2. Medium    $7.99");
            Console.WriteLine("3. Large     $9.99");
            Console.Write("Please choose a size: ");

            int input = UserInput();

            switch (input)
            {
                case 1:
                    return "Small";
                case 2:
                    return "Medium";
                case 3:
                    return "Large";
                default:
                    return ChooseSize();
            }
        }

        static string ChooseCrust()
        {
            Console.WriteLine("CRUSTSSsssss: ");
            Console.WriteLine("1. DeepDish");
            Console.WriteLine("2. Thin");
            Console.WriteLine("3. Cheese-stuffed");
            Console.Write("Please choose a crust: ");

            int input = UserInput();

            switch (input)
            {
                case 1:
                    return "DeepDish";
                case 2:
                    return "Thin";
                case 3:
                    return "Cheese-stuffed";
                default:
                    return ChooseCrust();       
            }
        }
        static string ChooseTopping()
        {
            Console.WriteLine("TOPPINGSSssss: ");
            Console.WriteLine("1. Pepperoni");
            Console.WriteLine("2. Sausage");
            Console.WriteLine("3. Buffalo");
            Console.Write("Choose topping: ");

            int input = UserInput();

            switch (input)
            {
                case 1:
                    return "Pepperoni";
                case 2:
                    return "Sausage";
                case 3:
                    return "Buffalo";
                default:
                   return ChooseTopping();
            }
        }

        static void ViewStoreOrders(ManagerDetails man)
        {
            Console.Clear();
            man.CurrentStore.ViewStoreOrders();
            MDash(man);

        }

        static void ChangeCurrentStore(ManagerDetails man)
        {
            man.CurrentStore = ChooseStore();
            MDash(man);
        }

        static StoreDetails SelectStore()
        {
            return new StoreDetails("Best Pizza ", "in The World");
        }

        public static int UserInput()
        {
            int input;
            try
            {
                input = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception e)
            {
                var a = e.Data;
                Console.WriteLine("Invalid input\nTry Again");
                return -1;
            }
            return input;
        }
    }
}
