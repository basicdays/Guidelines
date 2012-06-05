using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;
using AutoMapper;

namespace Guidelines.AutoMapper.Resolvers
{
	/// <summary>
	/// I had to tweek automapper to make this work so Im just commenting this out for now.
	/// What this does is allow you to create a convention for mapping based on a property name.
	/// 
	/// -- So something like this
	///   DateThingResolver<ObjTypeOne, ObjTypeTwo>
	///		Source Property = "DateThing"
	///		Destination Property = "ModifiedDateThing"
	/// 
	///   Source Object Definition
	///		ObjTypeOne SuperDateThing {get; set;}
	///		ObjTypeOne UglyDateThing {get; set;}
	/// 
	///	  Destination Object Definition
	///		ObjTypeTwo ModfiedSuperDateThing {get; set;}
	///		ObjTyObjTypeTwo ModfiedUglyDateThing {get; set;}
	/// 
	///   Mapping Config
	///		.ForMember(dest => dest.ModfiedSuperDateThing, opt => opt.ResolveUsing<DateThingResolver>()) 
	///		.ForMember(dest => dest.ModfiedUglyDateThing, opt => opt.ResolveUsing<DateThingResolver>()) 
	/// 
	/// </summary>
	/// <typeparam name="TGather">Source property type</typeparam>
	/// <typeparam name="TDestination">Destination property type</typeparam>
	//public abstract class ByPropertyResolver<TGather, TDestination> : IValueResolver
	//{
	//    public abstract string BaseSourcePropertyName { get; }
	//    public abstract string BaseDestinationPropertyName { get; }

	//    private static readonly ConcurrentDictionary<string, Func<object, TGather>> _resolvers = new ConcurrentDictionary<string, Func<object, TGather>>();

	//    public ResolutionResult Resolve(ResolutionResult source)
	//    {
	//        var destinationName = source.DestinationName;
	//        var keyName = destinationName + source.Type.Name;

	//        Func<object, TGather> getter = _resolvers.GetOrAdd(keyName, key => CreateGetter(destinationName, source));
            
	//        TGather lookupValue = getter(source.Value);

	//        return source.New(ResolveCore(lookupValue), typeof(TDestination));
	//    }

	//    private Func<object, TGather> CreateGetter(string destinationName, ResolutionResult source)
	//    {
	//        var sourceName = destinationName.Replace(BaseDestinationPropertyName, BaseSourcePropertyName);

	//        //get the source property
	//        Type sourceType = source.Value.GetType();
	//        PropertyInfo property = sourceType.GetProperty(sourceName);

	//        if (property == null)
	//        {
	//            throw new AutoMapperMappingException(source.Context, string.Format("Property {0} does not exist on {1}", sourceName, sourceType));
	//        }

	//        // whe are creating this -> sourceObject => sourceObject.EmployeeId
	//        ParameterExpression parameterArgument = Expression.Parameter(typeof(object), "sourceObject"); // this is the input to the lambda
	//        Expression cast = Expression.TypeAs(parameterArgument, sourceType); //were passing in the input as an object so cast it to its actual type
	//        Expression getEmployeeIdProperty = Expression.Property(cast, property); // Get the property

	//        return Expression.Lambda<Func<object, TGather>>(getEmployeeIdProperty, parameterArgument).Compile();
	//    }

	//    protected abstract TDestination ResolveCore(TGather gatheredProperty);
	//}
}
