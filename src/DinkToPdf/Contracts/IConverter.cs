using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DinkToPdf.EventDefinitions;

namespace DinkToPdf.Contracts
{
    public interface IConverter
    {

        /// <summary>
        ///  Converts document based on given settings
        /// </summary>
        /// <param name="document">Document to convert</param>
        /// <returns>Returns converted document as a Stream</returns>
        Stream Convert(IDocument document);

        event EventHandler<PhaseChangedArgs> PhaseChanged;

        event EventHandler<ProgressChangedArgs> ProgressChanged;

        event EventHandler<FinishedArgs> Finished;

        event EventHandler<ErrorArgs> Error;

        event EventHandler<WarningArgs> Warning;

    }
}
