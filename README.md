# AutoMapper.Attributes
A convenient way to create AutoMapper type mappings using attributes.

### How to use ###
1. Create the classes you'd like to map.
2. Add the `[MapsTo]` attribute to the source class, with the destination type as the argument. (Alternatively, you can use the `[MapsFrom]` attribute to map the destination class with the source type.)
3. Call the `MapTypes()` extension method on the assembly from which you want to map your types.
4. You're done!

### Sample ###

	public class Program
	{
		public void Main()
		{
			typeof(Program).Assembly.MapTypes();
			var person = new Person { FirstName = "John", LastName = "Lackey" };
			var customer = AutoMapper.Mapper.Map<Customer>(person);
			
			Console.WriteLine(customer.LastName);
			// Output: Lackey
		}
	
		[MapsTo(typeof(Customer))]
	    public class Person 
		{
			public string FirstName { get; set; }
			public string LastName { get; set; }
			public string Notes { get; set; }
		}
	
		public class Customer
		{
			public string FirstName { get; set; }
			public string LastName { get; set; }
			public string CustomerNotes { get; set; }
		}
	}

### Why? ###
AutoMapper is awesome.  However, in order to use it you have to call `Mapper.CreateMap()` for every two types you want to map.  This makes it a bit easier and cleaner for my use cases.

### Ok, but I need some custom mapping behavior. ###
No problem - you can create a subclass of `MapsToAttribute` and override the ConfigureMapping method.  The mapping you create can then be customized, like so:

	public class MapsToCustomer : MapsToAttribute
    {
        public MapsToCustomer() : base(typeof(Customer)) {}

        public override void ConfigureMapping(IMappingExpression mappingExpression)
        {
            mappingExpression.ForMember("CustomerNotes", expression => expression.MapFrom("Notes"));
        }
    }

### But I liked being able to use strongly typed expressions to configure my mappings. ###
Also not a problem - you can create a subclass of `MapsToAttribute` and add a custom ConfigureMapping method with the type arguments specified explicitly for `IMappingExpression`. As long as the method is named ConfigureMapping and the method signature uses the exact same type arguments as the classes you're mapping, you can configure the mapping to your heart's content:

	public class MapsToCustomer : MapsToAttribute
    {
        public MapsToCustomer() : base(typeof(Customer)) {}

        public void ConfigureMapping(IMappingExpression<Person, Customer> mappingExpression)
        {
            mappingExpression.ForMember(d => d.CustomerNotes, expression => expression.MapFrom(s => s.Notes));
        }
    }

### What else you got?

* The examples above make heavy use of the `[MapsTo]` attribute, but don't forget you have a `[MapsFrom]` attribute as well!
* You can set the ReverseMap property in the `[MapsTo]`/`[MapsFrom]` attribute to `true` if you want to map the classes in reverse as well, like this:

		[MapsTo(typeof(Customer), true)]	//this will map Customer to Person and Person to Customer
	    public class Person {...}

### This seems like a lot of work.  Why not just call `Mapper.CreateMap()`?

Simply put - I love anything that saves me keystrokes.  This is configurable enough for my needs and allows me to separate concerns nicely.

### License ###
This project is licensed under the MIT license.