using n1word_api.Models;

namespace n1word_api.Interfaces
{
    public interface IWordRepository
    {
        int CreatWord(List<Word> words);
        List<Word> GetWord();
    }
}
