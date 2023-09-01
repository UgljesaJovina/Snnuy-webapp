// deprecated


namespace Repositories.Models;

class CustomKeywords
{
    public Guid Id { get; set; } = Guid.NewGuid(); 
    public string KeywordName { get; set; }
    public string KeywordEffect { get; set; }
}