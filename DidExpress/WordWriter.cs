using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Word = Microsoft.Office.Interop.Word;
using System.Windows;
using Google.Protobuf.WellKnownTypes;

namespace DidExpress {
    class WordWriter {
        public static string WriteToFile(int age, Dictionary<int, int> data, string saveDir = null) {
            if (saveDir == null) {
                saveDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            }

            if (!Directory.Exists(saveDir)) {
                try {
                    Directory.CreateDirectory(saveDir);
                }
                catch (Exception ex) {
                    return null;
                }
            }

            var template = new FileInfo(Path.Combine(Environment.CurrentDirectory, "Templates\\Шаблон_Пошуку_Іграшок.dot"));

            var wordApp = new Word.Application();

            try {
                var doc = wordApp.Documents.Open(template.FullName);

                doc.Content.Find.Execute(FindText: "<AGE>", ReplaceWith: age.ToString());

                var table = doc.Tables[1];

                foreach (var item in data) {
                    var row = table.Rows.Add();
                    row.Cells[1].Range.Text = item.Key.ToString();
                    row.Cells[2].Range.Text = item.Value.ToString();
                }

                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string saveFileName = $"Результати пошуку іграшок_{timestamp}.doc";
                string savePath = Path.Combine(saveDir, saveFileName);
                doc.SaveAs(savePath);

                wordApp.ActiveDocument.Close();
                wordApp.Quit();

                return savePath;
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }

            if (wordApp != null) {
                wordApp.Quit();
            }

            return null;
        }
    }
}
