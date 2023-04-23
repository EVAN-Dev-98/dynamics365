using System;
using System.Net;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Crm.Sdk.Messages;
using System.ServiceModel.Description;
using System.Collections.Generic;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using System.Text;

namespace D365CRUD
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            IOrganizationService service = getCRMService();

            if (service != null)
            {
                Console.WriteLine("Enter Number Desired Operations (1,2,3,4,5,6)");
                Console.WriteLine("1 = Create Entity Record");
                Console.WriteLine("2 = Update Entity Record");
                Console.WriteLine("3 = Delete Entity Record");
                Console.WriteLine("4 = Retrieve a Record Data of Entity");
                Console.WriteLine("5 = Retrieve All Records of Entity");
                Console.WriteLine("6 = Retrieve All Entities");
                string response = Console.ReadLine();
                string entity_number = "";
                switch (response)
                {
                    case "1":
                        // Create Entity Record
                        entity_number = select_entity();
                        switch (entity_number)
                        {
                            case "1":
                                #region CreateAccount
                                Console.WriteLine("Type the Name Of New Account :");
                                String AccountName = Console.ReadLine();
                                Entity account = new Entity("account");
                                account["name"] = AccountName;
                                service.Create(account);
                                Console.WriteLine("New Account Created SuccessFully!");
                                break;
                            #endregion
                            case "2":
                                #region CreateContact
                                Console.WriteLine("Type the First Name Of New Contact :");
                                String FirstName = Console.ReadLine();
                                Console.WriteLine("Type the Last Name Of New Contact :");
                                String LastName = Console.ReadLine();
                                Entity contact = new Entity("contact");
                                contact["firstname"] = FirstName;
                                contact["lastname"] = LastName;
                                service.Create(contact);
                                Console.WriteLine("New Contact Created SuccessFully!");
                                break;
                            #endregion
                            default:
                                break;
                        }
                        break;
                    case "2":
                        // Update Entity Record
                        entity_number = select_entity();
                        switch (entity_number)
                        {
                            case "1":
                                #region UpdateAccount
                                Show_All_Records(service, "account", new string[] { "accountid", "name" });
                                
                                Console.WriteLine("Copy ID of Your Account's Want to Update!");
                                Console.Write("Enter Account ID : ");
                                string AccountID = Console.ReadLine();
                                Entity retrievedAccount = new Entity("account", new Guid(AccountID));
                                Entity UpdatedEntity1 = new Entity("account");
                                UpdatedEntity1.Id = retrievedAccount.Id;
                                Console.WriteLine("Enter New Account Name :");
                                UpdatedEntity1.Attributes["name"] = Console.ReadLine();
                                service.Update(UpdatedEntity1);
                                Console.WriteLine("Account Updated SuccessFully!");
                                break;
                            #endregion
                            case "2":
                                #region UpdateContact
                                Show_All_Records(service, "contact", new string[] { "contactid", "firstname", "lastname" });

                                Console.WriteLine("Copy ID of Your Contact's Want to Update!");
                                Console.Write("Enter Contact ID : ");
                                string ContactID = Console.ReadLine();
                                Entity retrievedContact = new Entity("contact", new Guid(ContactID));
                                Entity UpdatedEntity2 = new Entity("contact");
                                UpdatedEntity2.Id = retrievedContact.Id;
                                Console.WriteLine("Enter New First Name :");
                                UpdatedEntity2.Attributes["firstname"] = Console.ReadLine();
                                Console.WriteLine("Enter New Last Name :");
                                UpdatedEntity2.Attributes["lastname"] = Console.ReadLine();
                                service.Update(UpdatedEntity2);
                                Console.WriteLine("Contact Updated SuccessFully!");
                                break;
                            #endregion
                            default:
                                break;
                        }
                        break;
                    case "3":
                        // Delete Entity Record
                        entity_number = select_entity();
                        switch (entity_number)
                        {
                            case "1":
                                #region DeleteAccount
                                Show_All_Records(service, "account", new string[] { "accountid", "name" });

                                Console.WriteLine("Copy ID of Your Account's Want to Delete!");
                                Console.Write("Enter Account ID : ");
                                string AccountID = Console.ReadLine();
                                Entity retrievedAccount = new Entity("account", new Guid(AccountID));
                                service.Delete("account",retrievedAccount.Id);
                                Console.WriteLine("Account Deleted SuccessFully!");
                                break;
                            #endregion
                            case "2":
                                #region DeleteContact
                                Show_All_Records(service, "contact", new string[] { "contactid", "firstname", "lastname" });

                                Console.WriteLine("Copy ID of Your Contact's Want to Delete!");
                                Console.Write("Enter Contact ID : ");
                                string ContactID = Console.ReadLine();
                                Entity retrievedContact = new Entity("contact", new Guid(ContactID));
                                service.Delete("contact", retrievedContact.Id);
                                Console.WriteLine("Contact Deleted SuccessFully!");
                                break;
                            #endregion
                            default:
                                break;
                        }
                        break;
                    case "4":
                        // Retrieve a Record of Entity
                        entity_number = select_entity();
                        switch (entity_number)
                        {
                            case "1":
                                #region RetrieveAccount
                                Show_All_Records(service, "account", new string[] { "accountid", "name" });

                                Console.WriteLine("Copy ID of Your Account's Want to Retrieve!");
                                Console.Write("Enter Account ID : ");
                                string AccountID = Console.ReadLine();
                                Entity retrievedAccount = new Entity("account", new Guid(AccountID));
                                ColumnSet Att_A = new ColumnSet("name", "telephone1", "websiteurl", "address1_composite", "description", "primarycontactid");
                                Entity Account_R = service.Retrieve("account", retrievedAccount.Id, Att_A);

                                Console.WriteLine("---- Account Information ------------------------");
                                Console.WriteLine("Account Name : " + Account_R["name"]);
                                if (Account_R.Contains("telephone1"))
                                    Console.WriteLine("Phone : " + Account_R["telephone1"]);
                                if (Account_R.Contains("websiteurl"))
                                    Console.WriteLine("Website : " + Account_R["websiteurl"]);
                                if (Account_R.Contains("address1_composite"))
                                    Console.WriteLine("Address : " + Account_R["address1_composite"]);
                                if (Account_R.Contains("description"))
                                    Console.WriteLine("Description : " + Account_R["description"]);
                                break;
                            #endregion
                            case "2":
                                #region RetrieveContact
                                Show_All_Records(service, "contact", new string[] { "contactid", "firstname", "lastname" });

                                Console.WriteLine("Copy ID of Your Contact's Want to Retrieve!");
                                Console.Write("Enter Contact ID : ");
                                string ContactID = Console.ReadLine();
                                Entity retrievedContact = new Entity("contact", new Guid(ContactID));
                                ColumnSet Att_C = new ColumnSet("firstname", "lastname", "jobtitle", "emailaddress1", "telephone1", "mobilephone", "address1_composite");
                                Entity Contact_R = service.Retrieve("contact", retrievedContact.Id, Att_C);

                                Console.WriteLine("---- Contact Information ------------------------");
                                Console.WriteLine("Contact FirstName : " + Contact_R["firstname"]);
                                if (Contact_R.Contains("lastname"))
                                    Console.WriteLine("LastName : " + Contact_R["lastname"]);
                                if (Contact_R.Contains("jobtitle"))
                                    Console.WriteLine("Job Title : " + Contact_R["jobtitle"]);
                                if (Contact_R.Contains("emailaddress1"))
                                    Console.WriteLine("Email : " + Contact_R["emailaddress1"]);
                                if (Contact_R.Contains("telephone1"))
                                    Console.WriteLine("Business Phone : " + Contact_R["telephone1"]);
                                if (Contact_R.Contains("mobilephone"))
                                    Console.WriteLine("Mobile Phone : " + Contact_R["mobilephone"]);
                                if (Contact_R.Contains("address1_composite"))
                                    Console.WriteLine("Address : " + Contact_R["address1_composite"]);
                                break;
                            #endregion
                            default:
                                break;
                        }
                        break;
                    case "5":
                        // Retrieve All Records of Entity
                        entity_number = select_entity();
                        switch (entity_number)
                        {
                            case "1":
                                Show_All_Records(service, "account", new string[] { "accountid", "name" });
                                break;
                            case "2":
                                Show_All_Records(service, "contact", new string[] { "contactid", "firstname", "lastname" });
                                break;
                            default:
                                break;
                        }
                        break;
                    case "6":
                        // Retrieve All Entities Logical Name
                        Console.WriteLine("Please Wait to Get All Entities ...");
                        var request_x = new RetrieveAllEntitiesRequest { EntityFilters = EntityFilters.Entity };
                        var response_x = (RetrieveAllEntitiesResponse)service.Execute(request_x);
                        int counter = 0;
                        Console.WriteLine("---------------------------------------");
                        foreach (EntityMetadata em in response_x.EntityMetadata)
                        {
                            counter++;
                            Console.WriteLine(counter + " : " + em.LogicalName);
                        }
                        break;
                    default:
                        break;
                }
            }
            Console.ReadLine();
        }
        // Connection To D365
        public static IOrganizationService getCRMService()
        {
            IOrganizationService organizationService = null;
            String username = "s.gholami";
            String password = "Aa@123456";
            string url = "http://172.16.22.5/Test/XRMServices/2011/Organization.svc";
            try
            {
                ClientCredentials clientCredentials = new ClientCredentials();
                clientCredentials.UserName.UserName = username;
                clientCredentials.UserName.Password = password;

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                organizationService = (IOrganizationService)new OrganizationServiceProxy(new Uri(url), null, clientCredentials, null);

                if (organizationService != null)
                {
                    Guid guid = ((WhoAmIResponse)organizationService.Execute(new WhoAmIRequest())).OrganizationId;
                    if (guid != Guid.Empty)
                    {
                        Console.WriteLine("Connection Successfully!");
                    }
                }
                else
                {
                    Console.WriteLine("Connection Faild!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception : " + ex.Message);
            }
            return organizationService;
        }

        public static string select_entity()
        {
            Console.WriteLine("Select Entity For Create new Record : (1,2)");
            Console.WriteLine("1 = Account");
            Console.WriteLine("2 = Contact");
            return Console.ReadLine();
        }

        public static void Show_All_Records(IOrganizationService service,string EntityName, string[] Columns)
        {
            Console.WriteLine("Please Wait to Get All Records ...");
            QueryExpression query = new QueryExpression(EntityName);
            query.ColumnSet.AddColumns(Columns);

            EntityCollection result = service.RetrieveMultiple(query);

            var all_records = new Dictionary<string, string>();

            foreach (var Record in result.Entities)
            {
                if (Columns.Length > 2)
                    all_records.Add(Record.Attributes[Columns[0]].ToString(), Record.Attributes[Columns[1]] + " " + Record.Attributes[Columns[2]]);
                else
                    all_records.Add(Record.Attributes[Columns[0]].ToString(), Record.Attributes[Columns[1]].ToString());
            }

            Console.WriteLine("---------------------------------------");
            foreach (KeyValuePair<string, string> item in all_records)
            {
                Console.WriteLine("Name : " + item.Value);
                Console.WriteLine("ID : " + item.Key);
                Console.WriteLine("---------------------------------------");
            }
        }
    }
}