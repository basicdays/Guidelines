namespace Guidelines.Core.Commands
{
	public interface IQueryHandler<in TCommand, out TResponse>
	{
		TResponse Execute(TCommand commandMessage);
	}

	/*So this is the structure we have
     * Update-
     *  Get existing entity
     *  Does it exists
     *  Do I have permision to change it
     *  Update the entity (map from command)?
     *  Is the entity still valid
     *  Save the entity
     *  
     * This should be able to be automated
     * What we need
     *  A Base entity that has an Id
     *  Base repository for entity with get by Id and update/insert/delete
     *  Base interface for command for update/insert/delete/read
     *  permisions interface for editing object
     *  permisions interface specific to command -> seperate for update/insert/delete?
     *  base implementation of handlers using privleg objects
     *      -allow interface to extend actual edit/create (work) function
     *      -way to chose user defined methods over default functionality
     *      -allow base handler for those without work command interfaces
     *  Way to generate default mappings if mapping doesnt exist
     *      -perfer custom definition before loading default mapping
     *      
     * Goal:
     *  I want to be able to declare a command object and thoss on an interaface and have it update the object
     *  I want to be able to declare a command and several permisions interfaces and have it check those then update
     *  I want to be able to implement and interface that all I need to do is the updating work, no privs or null checks or validating
    */
}
