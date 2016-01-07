# AutoMapper.Attributes
A convenient way to create AutoMapper type mappings using attributes.

[![Build status](https://ci.appveyor.com/api/projects/status/fkedmn5vx1j9ne5x?svg=true)](https://ci.appveyor.com/project/schneidsDotNet/automapper-attributes)

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
			public string MyCustomerNotes { get; set; }
		}
	}

### Custom property mapping

There are two attributes - `[MapsFromProperty]` and `[MapsToProperty]`.  Both take a type and a name of a property you want to map from/to.  These attributes even support nested property mapping using normal dot notation.

	[MapsFromProperty(typeof(SourceType), "Address.City")]

Examples below:

#### MapsToProperty example

`[MapsToProperty]` allows you to designate a source type's property as mapping to a destination type's property.  Simply: 

1. Define your source class (in this example, Person)
2. Define your destination class (in this example, Customer)
2. Define your source class properties
3. Add the `[MapsToProperty]` to the property that you want to map to the destination property.  In this case, the attribute's arguments are the destination type and the name of the destination type's property.

##### Sample

	public class Program
	{
		public void Main()
		{
			typeof(Program).Assembly.MapTypes();
			var person = new Person { Notes = "these are some notes" };
			var customer = AutoMapper.Mapper.Map<Customer>(person);
			
			Console.WriteLine(customer.MyCustomerNotes);
			// Output: these are some notes
		}
	
		[MapsTo(typeof(Customer))]
	    public class Person 
		{
			public string FirstName { get; set; }
			public string LastName { get; set; }
			[MapsToProperty(typeof(Customer), "MyCustomerNotes")]
			public string Notes { get; set; }
		}
	
		public class Customer
		{
			public string FirstName { get; set; }
			public string LastName { get; set; }
			public string MyCustomerNotes { get; set; }
		}
	}

#### MapsFromProperty example

`[MapsFromProperty]` allows you to designate a destination type's property as mapping to a source type's property.  Simply: 

1. Define your source class (in this example, Person)
2. Define your destination class (in this example, Customer)
2. Define your source class properties
3. Add the `[MapsFromProperty]` to the destination type's property that you want to map to the source property.  In this case, the attribute's arguments are the source type and the name of the source type's property.

##### Sample

	public class Program
	{
		public void Main()
		{
			typeof(Program).Assembly.MapTypes();
			var person = new Person { Notes = "these are some more notes" };
			var customer = AutoMapper.Mapper.Map<Customer>(person);
			
			Console.WriteLine(customer.MyCustomerNotes);
			// Output: these are some more notes
		}
	
	    public class Person 
		{
			public string FirstName { get; set; }
			public string LastName { get; set; }
			public string Notes { get; set; }
		}
	
		[MapsFrom(typeof(Person))]
		public class Customer
		{
			public string FirstName { get; set; }
			public string LastName { get; set; }
			[MapsFromProperty(typeof(Person), "Notes")]
			public string MyCustomerNotes { get; set; }
		}
	}



### Ok, but I need some super custom mapping behavior. ###
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

Simply put - I love anything that saves me keystrokes.  This is configurable enough for my needs and allows me to separate concerns nicely.  Plus, the property attribution makes it super easy and useful to provide custom mappings!

### License ###
This project is licensed under the MIT license.
