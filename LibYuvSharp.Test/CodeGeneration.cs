using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace Lennox.LibYuvSharp.Tests
{
    [TestFixture]
    public class CodeGeneration
    {
        private const string _path = @"C:\code2\libyuv";

        /// <summary>
        /// Generates the body of the LibYuv class from the libyuv source.
        /// </summary>
        [Test, Explicit]
        public void GenerateClassDefinition()
        {
            var headers = Directory.GetFiles(
                _path, "*.h", SearchOption.AllDirectories);

            var output = new StringBuilder();
            var comments = new List<string>();
            var definition = new StringBuilder();
            var indefintion = false;
            var injpeg = 0;
            var alreadyExtracted = new HashSet<string>();

            var extractName = new Regex(@"[^ ]+(?=\()", RegexOptions.Compiled);

            void ClearAll()
            {
                comments.Clear();
                definition.Clear();
                indefintion = false;
                injpeg = 0;
            }

            foreach (var file in headers)
            {
                if (file.Contains("third_party")) continue;

                var lines = File.ReadAllLines(file);
                for (var i = 0; i < lines.Length; ++i)
                {
                    var line = lines[i].Trim();

                    if (string.IsNullOrWhiteSpace(line)) continue;

                    // Skip lines inside of the HAVE_JPEG ifdef because we
                    // do not compile with it.
                    if (line == "#ifdef HAVE_JPEG")
                    {
                        injpeg = 1;
                        continue;
                    }

                    if (injpeg > 0)
                    {
                        if (line.StartsWith("#ifdef")) ++injpeg;
                        else if (line == "#endif") --injpeg;

                        continue;
                    }

                    if (line.StartsWith("//"))
                    {
                        comments.Add(line.TrimStart('/', ' '));
                        continue;
                    }

                    if (line == "LIBYUV_API")
                    {
                        indefintion = true;
                        continue;
                    }

                    if (indefintion)
                    {
                        if (line.EndsWith(","))
                        {
                            definition.AppendLine(line);
                            continue;
                        }

                        if (line.EndsWith(";"))
                        {
                            definition.AppendLine(line);

                            var stringDef = definition.ToString();
                            var name = extractName.Match(stringDef).Value;

                            // There's a lot of duplicates.
                            if (!alreadyExtracted.Add(name))
                            {
                                ClearAll();
                                continue;
                            }

                            output.AppendFormat("// {0}:{1}\n", file, i);
                            output.AppendLine("/// <summary>");
                            foreach (var comment in comments) output.AppendLine("/// " + comment);
                            output.AppendLine("/// </summary>");

                            var def = stringDef
                                .Replace("const ", "")
                                .Replace("enum ", "")
                                .Replace("LIBYUV_BOOL", "int")
                                .Replace("int64_t", "long")
                                .Replace("int32_t", "int")
                                .Replace("int16_t", "short")
                                .Replace("uint8_t", "byte")
                                .Replace("int8_t", "byte")
                                .Replace("size_t", "IntPtr")
                                .Replace("ARGBBlendRow", "void*")
                                .Replace("(void)", "()");

                            output.AppendLine("[DllImport(_path, SetLastError = true)]");
                            output.Append("public static extern ");
                            output.AppendLine(def);

                            ClearAll();
                        }
                    }
                }

                ClearAll();
            }

            File.WriteAllText(Path.Combine(_path, "generated.cs"), output.ToString());
        }
    }
}