using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using MemeGenerator;
using System.IO;

namespace sarcasticBob
{
    public class Utility
    {
        private MemeGenerator.MemeGenerator Generator { get; set; }

        public Utility()
        {
            Generator = new MemeGenerator.MemeGenerator(); 
        }


        public async Task<string> GetLastMessageAsync(CommandContext ctx, DiscordMember member)
        {
            var messages = await ctx.Channel.GetMessagesAsync(around: ctx.Channel.LastMessageId, limit: 100);

            return messages.First(message => message.Author.Id.Equals(member.Id)).Content;
        }

        public string CreateSpongeBob(string text)
        {
            var separatedText = splitText(text);
            var topTextList = separatedText[0];
            var bottomTextList = separatedText[1];

            var topText = "";
            var bottomText = ""; 

            foreach(var word in topTextList) { topText += Sarcastify(word); }
            foreach (var word in bottomTextList) { bottomText += Sarcastify(word); }

            var memePath = Generator.CreateMeme(@"spongebob.jpg", topText, bottomText);

            return memePath;
        }

        private string Sarcastify(string word)
        {
            word = word.ToLower();
            var newWord = "";
            for (var i = 0; i < word.Length; i++)
            {
                var letter = word[i];
                if (i % 2 == 0)
                {
                    letter = Char.ToUpper(letter);
                }

                newWord += letter; 
            }

            newWord += " ";

            return newWord;
        }

        private List<List<string>> splitText(string text)
        {
            var words = text.Split(' ');
            var halfway = words.Count() / 2;
            var topText = new List<string>();
            var bottomText = new List<string>();
            var separatedWords = new List<List<string>>();

            for(var i= 0;i<words.Length; i++)
            {
                if (i <= halfway)
                {
                    topText.Add(words[i]);
                }
                else
                {
                    bottomText.Add(words[i]);
                }
            }

            separatedWords.Add(topText);
            separatedWords.Add(bottomText);

            return separatedWords;
        }
    }
}
