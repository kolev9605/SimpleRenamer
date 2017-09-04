namespace Renamer
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    public class CyrillicRenamer
    {
        private Dictionary<string, string> CirilycToLatin = new Dictionary<string, string>()
        {
            { "А", "A"},
            { "Б", "B"},
            { "В", "V"},
            { "Г", "G"},
            { "Д", "D"},
            { "Е", "E"},
            { "Ж", "J"},
            { "З", "Z"},
            { "И", "I"},
            { "Й", "I"},
            { "К", "K"},
            { "Л", "L"},
            { "М", "M"},
            { "Н", "N"},
            { "О", "O"},
            { "П", "P"},
            { "Р", "R"},
            { "С", "S"},
            { "Т", "T"},
            { "У", "U"},
            { "Ф", "F"},
            { "Х", "H"},
            { "Ц", "C"},
            { "Ч", "Ch"},
            { "Ш", "Sh"},
            { "Щ", "Sht"},
            { "Ъ", "U"},
            { "Ю", "Yu"},
            { "Я", "Q"},
        };

        public void Rename(string directory)
        {
            string[] allfiles = Directory.GetFiles(directory, "*.*", SearchOption.AllDirectories);
            IEnumerable<MusicFile> musicFilesToBeRenamed = allfiles
                .Select(mf => mf.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries))
                .Select(mf => new MusicFile
                {
                    Directory = string.Join("\\", mf.Take(mf.Length - 1)),
                    FileName = mf[mf.Length - 1],
                    FullDirectory = string.Join("\\", mf)
                })
                .Where(mf => Regex.IsMatch(mf.FileName, @"\p{IsCyrillic}"));

            this.ExecuteRename(musicFilesToBeRenamed);            
        }

        public void Rename(IEnumerable<MusicFile> musicFiles)
        {
            IEnumerable<MusicFile> musicFilesToBeRenamed = musicFiles
                .Where(mf => Regex.IsMatch(mf.FileName, @"\p{IsCyrillic}"));

            this.ExecuteRename(musicFilesToBeRenamed);
        }

        private void ExecuteRename(IEnumerable<MusicFile> musicFilesToBeRenamed)
        {
            Console.WriteLine("====================================");
            Console.WriteLine("There are {0} files to be renamed", musicFilesToBeRenamed.Count());
            Console.WriteLine("====================================");

            foreach (var musicFile in musicFilesToBeRenamed)
            {
                string renameResult = this.RenameWord(musicFile.FileName);
                Console.WriteLine("({0})", musicFile.Directory);
                Console.WriteLine(
                    "Do you wish to rename:{0} -{1}{0}To:{0} -{2}{0}(Y/N)",
                    Environment.NewLine,
                    musicFile.FileName,
                    renameResult);

                string command = Console.ReadLine();
                if (command.ToLower() == "y" || command == string.Empty)
                {
                    File.Move(musicFile.FullDirectory, musicFile.Directory + "\\" + renameResult);
                }
                else
                {
                    continue;
                }
            }
        }

        private string RenameWord(string input)
        {
            StringBuilder output = new StringBuilder();
            foreach (var letter in input)
            {
                bool isLowercase = char.IsLower(letter);
                string uppercaseLetter = letter.ToString().ToUpper();
                if (this.CirilycToLatin.ContainsKey(uppercaseLetter))
                {
                    if (isLowercase)
                    {
                        output.Append(this.CirilycToLatin[uppercaseLetter].ToLower());
                    }
                    else
                    {
                        output.Append(this.CirilycToLatin[uppercaseLetter]);
                    }
                }
                else
                {
                    output.Append(letter.ToString());
                }
            }

            return output.ToString();
        }
    }
}
