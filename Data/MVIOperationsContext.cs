namespace MVIOperationsSystem.Data
{
	using MVIOperations.Models;
	using MVIOperationsSystem.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;

    public class MVIOperationsContext : DbContext
    {
        // Your context has been configured to use a 'MVIOperationsContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'MVIOperationsSystem.Data.MVIOperationsContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'MVIOperationsContext' 
        // connection string in the application configuration file.
        public MVIOperationsContext()
            : base("name=MVIOperationsContext")
        {
            //if (MVIOperationsContext.Database.CanConnect())
            //{
            //    // all good
            //}

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

		internal void Add(Error error)
		{
			throw new NotImplementedException();
		}

		// Add a DbSet for each entity type that you want to include in your model. For more information 
		// on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

		public virtual DbSet<Country> Country{ get; set; }
        public virtual DbSet<District> District { get; set; }
        public virtual DbSet<Region> Region { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<EmployeeTime> EmployeeTime { get; set; }
        public virtual DbSet<Gender> Gender { get; set; }
        public virtual DbSet<Race> Race { get; set; }
        public virtual DbSet<Inventory> Inventory { get; set; }
        public virtual DbSet<Store> Store { get; set; }
        public virtual DbSet<Sale> Sale { get; set; }
        public virtual DbSet<SaleItem> SaleItem { get; set; }
        public virtual DbSet<State> State { get; set; }
        public virtual DbSet<Error> Error { get; set; }


    }
}