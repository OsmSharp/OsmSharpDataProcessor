﻿// OsmSharp - OpenStreetMap (OSM) SDK
// Copyright (C) 2015 Abelshausen Ben
// 
// This file is part of OsmSharp.
// 
// OsmSharp is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 2 of the License, or
// (at your option) any later version.
// 
// OsmSharp is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with OsmSharp. If not, see <http://www.gnu.org/licenses/>.

using OsmSharp.Osm.PBF.Streams;
using OsmSharpDataProcessor.Processors;
using System.IO;

namespace OsmSharpDataProcessor.Commands
{
    /// <summary>
    /// A PBF reading command.
    /// </summary>
    public class CommandReadPBF : Command
    {
        /// <summary>
        /// Returns the switches for this command.
        /// </summary>
        /// <returns></returns>
        public override string[] GetSwitch()
        {
            return new string[] { "--rb", "--read-pbf" };
        }

        /// <summary>
        /// The file to read from.
        /// </summary>
        public string File { get; set; }

        /// <summary>
        /// Parse the command arguments for the read-pbf command.
        /// </summary>
        /// <param name="args"></param>
        /// <param name="idx"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public override int Parse(string[] args, int idx, out Command command)
        {
            // check next argument.
            if (args.Length < idx)
            {
                throw new CommandLineParserException("None", "Invalid file name for read-pbf command!");
            }

            // everything ok, take the next argument as the filename.
            command = new CommandReadPBF()
            {
                File = args[idx]
            };
            return 1;
        }

        /// <summary>
        /// Creates the processor that corresponds to this command.
        /// </summary>
        /// <returns></returns>
        public override ProcessorBase CreateProcessor()
        {
            var file = new FileInfo(this.File);
            var source = new PBFOsmStreamSource(file.OpenRead());
            source.Meta.Add("filename", file.Name);

            return new ProcessorSource(source);
        }

        /// <summary>
        /// Returns a description of this command.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("--read-pbf {0}", this.File);
        }
    }
}
