using System.Text.RegularExpressions;
using PPDL;
using PPModel;

namespace PPBL
{

    public class PlanetPaintballStoresBL : IPlanetPaintballStoresBL
    {

        private IRepository _repo;

        public PlanetPaintballStoresBL(IRepository p_repo)
        {
            _repo = p_repo;
        }

        public List<StoreFront> ViewAllStores()
        {

            List<StoreFront> listOfAllStores = _repo.GetStoreFronts();
            return listOfAllStores;
        
        }

        public List<StoreFront> ViewInventory(string p_address)
        {

            List<StoreFront> listOfStores = _repo.GetStoreFronts();

            var found = listOfStores.Find(p => p.Address.Equals(p_address));
            if(found != null)
            {
                //validation process using LINQ Library
                return listOfStores
                    .Where(store => store.Address.Equals(p_address))
                    .ToList();
            }
            else
            {
                throw new Exception("A store with this address has not been found.");
            }

        }

        public List<Products> GetProductsByStoreAddress(string p_address)
        {
            return _repo.GetProductsByStoreAddress(p_address);
        }

        public void UpdateInventory(int p_storeID, int p_productID, int p_quantity)
        {
        _repo.UpdateInventory(p_storeID, p_productID, p_quantity);
        }

        public List<Products> ViewOrder(int p_productID, string p_address)
        {
            List<Products> listOfItemsInOrder = _repo.GetProductsByStoreAddress(p_address);
            
            return listOfItemsInOrder
                    .Where(product => product.ID.Equals(p_productID))
                    .ToList();

        }

        public LineItems MakeOrder(int p_storeID, LineItems p_lineItems, int quantity)
        {
            return _repo.MakeOrder(p_storeID, p_lineItems, quantity);
        }

        public Orders MakeAnOrder(Orders p_order)
        {
            return _repo.MakeAnOrder(p_order);
        }

        public List<Orders> GetOrders(string searchedString)
        {

            List<Customer> listOfCustomer = _repo.GetAllCustomers();
            List<StoreFront> listOfStores = _repo.GetStoreFronts();
            List<Orders> listOfOrders = _repo.GetAllOrders();
            List<LineItems> listOfLineItems = _repo.GetAllLineItems();

            //search by the customer email
            if(Regex.IsMatch(searchedString, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            {
                var found = listOfCustomer.Find(p => p.Email == searchedString);
                    if(found != null)
                    {

                        // //***needs work***
                        // //will add each of the line items to the orders history for customer.
                        // foreach(Orders orderItem in listOfOrders)
                        // {
                        //     foreach(LineItems item in listOfLineItems)
                        //     {
                        //         if(item.OrderID == orderItem.OrderID)
                        //         {
                        //             orderItem.LineItems.Add(item);
                        //         }
                        //     }
                        // }

                        //validation process using LINQ Library
                        return listOfOrders
                                .Where(customer => customer.CustomerID.Equals(found.ID))
                                .ToList();
                    }
                    else
                    {
                        throw new Exception("A customer with this email has not been found.");
                    }
            }

            //search by the store
            else if(Regex.IsMatch(searchedString, @"^[#.0-9a-zA-Z\s,-]+$"))
            {

                var found = listOfStores.Find(p => p.Address == searchedString);
                if(found != null)
                {

                    //validation process using LINQ Library
                    return listOfOrders
                        .Where(store => store.StoreID.Equals(found.ID))
                        .ToList();

                }
                else
                {
                    throw new Exception("A store with this address has not been found.");
                }

            } 
            else
            {
                throw new Exception("Could not search! Some error has occurred. Try restarting program.");
            }
            
        }

        public Orders StartOrder(Orders p_order)
        {
            return _repo.StartOrder(p_order);
        }

        public bool TestQuantity(int p_id, int p_quantity)
        {
            return _repo.TestQuantity(p_id, p_quantity);
        }

        public bool VerifyManager(string p_managerEmail, string p_managerPassword, int p_storeID)
        {

            List<Manager> listOfManagers = _repo.GetAllManagers();

            var found = listOfManagers.Find(p => p.Email.Equals(p_managerEmail) && p.Password.Equals(p_managerPassword) && p.storeID.Equals(p_storeID));
            if(found != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public Orders DeleteOrder(Orders p_order)
        {
            return _repo.DeleteOrder(p_order);
        }

        public List<Manager> GetManagers()
        {
            return _repo.GetAllManagers();
        }
    }

}