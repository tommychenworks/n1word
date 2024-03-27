using Microsoft.Extensions.Options;
using n1word_api.Interfaces;
using n1word_api.Models;
using Newtonsoft.Json;
using System.IO;

namespace n1word_api.Repositories
{
    
    public class WordRepository : IWordRepository
    {
        private readonly WordConfig _config;
        private string _filepath;
        private readonly ILogger<WordRepository> _logger;
        public WordRepository(IOptions<WordConfig> config, ILogger<WordRepository> logger)
        {
            _config = config.Value;
            _filepath = _config.jsonWordDB;
            _logger = logger;
        }

        /// <summary>
        /// 建立單字組
        /// </summary>
        /// <param name="words"></param>
        public int CreatWord(List<Word> words)
        {
            var originWord = GetWord();
            try
            {

                //加入筆數
                int addcount = originWord.Count;

                //直接加入
                string append = JsonConvert.SerializeObject(words);

                //與舊資料比對，將不重複加入
                if (originWord != null)
                {
                    var result = words
                        .Union(originWord)
                        .ToList()
                        .GroupBy(c => c)
                        .Select(c => c.First())
                        .ToList();

                    addcount = result.Count - addcount;

                    append = JsonConvert.SerializeObject(result);
                }

                //複寫
                var _path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _filepath);
                File.WriteAllText(_path, append);

                return addcount;
            }
            catch (Exception ex)
            {
                _logger.LogError("未知的錯誤：" + ex.Message);
                throw;
            }


        }

        /// <summary>
        /// 取得單字
        /// </summary>
        /// <returns></returns>
        public List<Word> GetWord()
        {
            try
            {
                string jsonstring = string.Empty;

                if (File.Exists(_filepath))
                {
                    jsonstring = File.ReadAllText(_filepath);
                }

                var words = JsonConvert.DeserializeObject<List<Word>>(jsonstring);

                if (words != null)
                {
                    return words;
                }
                else
                {
                    _logger.LogInformation("沒有單字！");
                    return new List<Word>();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("未知的錯誤：" + ex.Message);
                throw;
            }


        }
    }
}
