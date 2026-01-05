namespace htmx_examples.Pages.BulkUpdate;

public interface IContactService
{
    IEnumerable<Contact> Get();
    Contact? GetById(int id);
    void Update(int Id, bool Status);
}