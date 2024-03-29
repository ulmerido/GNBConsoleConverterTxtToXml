﻿using GNBConsoleConverterTxtToXml.Enteties;
using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace GNBConsoleConverterTxtToXml
{

    /// <summary>
    /// Convertering GNB ClientInfo text file to xml file
    /// </summary>
    public class Converter
    {
        const string k_XmlExtension = ".xml";

        public void Convert(string i_InputPath, string i_OutputPath = null, string i_outputFileName = null, string i_Encoding = "WINDOWS-1252")
        {
            if (i_OutputPath == null) i_OutputPath = String.Empty;
            if (i_outputFileName == null) i_outputFileName = Path.GetFileNameWithoutExtension(i_InputPath);

            var clientInfo = new ClientInfo(i_InputPath);
            var xmlSerializer = new XmlSerializer(typeof(ClientInfo));
            var xmlNameSpace = new XmlSerializerNamespaces();
            var xmlSettings = new XmlWriterSettings();
            var xdoc = new XmlDocument();
            if (i_OutputPath!= String.Empty && !i_OutputPath.EndsWith(Path.DirectorySeparatorChar.ToString())) i_OutputPath += Path.DirectorySeparatorChar;
            var outPath = i_OutputPath + i_outputFileName + k_XmlExtension;

            xmlNameSpace.Add(String.Empty, String.Empty);

            xmlSettings.Encoding = Encoding.GetEncoding(i_Encoding);
            xmlSettings.NewLineOnAttributes = true;
            xmlSettings.NewLineChars = Environment.NewLine;
            xmlSettings.Indent = true;
            xmlSettings.NamespaceHandling = NamespaceHandling.OmitDuplicates;

            using (XmlWriter writer = XmlWriter.Create(outPath, xmlSettings))
            {
                xmlSerializer.Serialize(writer, clientInfo, xmlNameSpace);
            }
        }
    }
}
