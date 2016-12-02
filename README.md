# AutoMapper.Attributes
A convenient way to create AutoMapper type mappings using attributes.

[![Build status](https://ci.appveyor.com/api/projects/status/fkedmn5vx1j9ne5x?svg=true)](https://ci.appveyor.com/project/schneidsDotNet/automapper-attributes)  [![Nuget](https://img.shields.io/nuget/v/Automapper.Attributes.svg)](https://www.nuget.org/packages/AutoMapper.Attributes/)

### How to use ###
1. Create the classes you'd like to map.
2. Add the `[MapsTo]` attribute to the source class, with the target type as the argument. (Alternatively, you can use the `[MapsFrom]` attribute to map the target class with the source type.)
3. Call the `MapTypes()` extension method on the assembly from which you want to map your types when you call `Mapper.Initialize`.
4. You're done!

### Sample ###

```csharp
public class Program
{
	public void Main()
	{
		AutoMapper.Mapper.Initialize(config => {
			typeof(Program).Assembly.MapTypes(config);
		});
		
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
```

### Mapping properties

AutoMapper's built-in property mapping works for 90% of use cases.  But what if you need to map two properties with different names, or even a property that is a few classes deep?  There are two attributes made for this - `[MapsFromProperty]` and `[MapsToProperty]`.  Both take a type and a name of a property you want to map from/to.  These attributes support nested property mapping using normal dot notation.

```csharp
[MapsFromProperty(typeof(SourceType), "Address.City")]
```

#### MapsToProperty example

`[MapsToProperty]` allows you to designate a source type's property as mapping to a target type's property.  Simply: 

1. Define your source class (in this example, Person)
2. Define your target class (in this example, Customer)
2. Define your source class properties
3. Add the `[MapsToProperty]` to the property that you want to map to the target property.  In this case, the attribute's arguments are the target type and the name of the target type's property.

##### Sample

```csharp
public class Program
{
	public void Main()
	{
		AutoMapper.Mapper.Initialize(config => {
			typeof(Program).Assembly.MapTypes(config);
		});

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
```

#### MapsFromProperty example

`[MapsFromProperty]` allows you to designate a target type's property as mapping to a source type's property.  Simply: 

1. Define your source class (in this example, Person)
2. Define your target class (in this example, Customer)
2. Define your source class properties
3. Add the `[MapsFromProperty]` to the target type's property that you want to map to the source property.  In this case, the attribute's arguments are the source type and the name of the source type's property.

##### Sample

```csharp
public class Program
{
	public void Main()
	{
		AutoMapper.Mapper.Initialize(config => {
			typeof(Program).Assembly.MapTypes(config);
		});

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
```

### Ok, but I need some super custom mapping behavior. ###
No problem - you can create a subclass of `MapsToAttribute` and add a method called `ConfigureMapping` method with the type arguments specified explicitly for `IMappingExpression`. As long as the method is named `ConfigureMapping` (case sensitive!) and the method signature uses the exact same type arguments as the classes you're mapping, you can configure the mapping to your heart's content:

```csharp
public class MapsToCustomer : MapsToAttribute
{
    public MapsToCustomer() : base(typeof(Customer)) {}

    public void ConfigureMapping(IMappingExpression<Person, Customer> mappingExpression)
    {
        mappingExpression.ForMember(d => d.CustomerNotes, expression => expression.MapFrom(s => s.Notes));
    }
}
```

### What else you got?

* The examples above make heavy use of the `[MapsTo]` attribute, but don't forget you have a `[MapsFrom]` attribute as well!
* You can set the ReverseMap property in the `[MapsTo]`/`[MapsFrom]` attribute to `true` if you want to map the classes in reverse as well, like this:

```csharp
[MapsTo(typeof(Customer), ReverseMap = true)]	//this will map Customer to Person and Person to Customer
public class Person {...}
```

### License ###
This project is licensed under the MIT license.
