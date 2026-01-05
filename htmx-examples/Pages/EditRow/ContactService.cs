namespace htmx_examples.Pages.EditRow;

public class ContactService : IContactService
{
    private List<Contact> contacts;

    public ContactService()
    {
        int key = 0;
        // Initialize the static contact member.
        contacts = new();
        contacts.Add(new(++key, "Scarlett Nolan", "scarlett.nolan@example.com"));
        contacts.Add(new(++key, "Leonardo Evans", "leonardo.evans   @example.com"));
        contacts.Add(new(++key, "Natalie Damon", "natalie.damon@example.com"));
        contacts.Add(new(++key, "Chris Johansson", "chris.johansson@example.com") { Status = false });
    }

    public void Update(Contact updatedContact)
    {
        var index = contacts.FindIndex(c => c.Id == updatedContact.Id);
        if (index != -1)
        {
            contacts[index] = updatedContact;
        }
    }

    public IEnumerable<Contact> Get()
    {
        return contacts;
    }

    public Contact Get(int Id)
    {
        return contacts.Single(c => c.Id == Id);
    }
}