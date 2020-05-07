﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;

namespace Microsoft.CodeAnalysis.LanguageServerIndexFormat.Generator.Graph
{
    /// <summary>
    /// Represents an event vertex for serialization. See https://github.com/Microsoft/language-server-protocol/blob/master/indexFormat/specification.md#events for further details.
    /// </summary>
    internal sealed class Event : Vertex
    {
        public string Kind { get; }
        public string Scope { get; }
        public Id<Element> Data { get; }

        private Event(EventKind kind, string scope, Id<Element> data)
            : base(label: "$event")
        {
            this.Kind = kind switch { EventKind.Begin => "begin", EventKind.End => "end", _ => throw new ArgumentException(nameof(kind)) };
            this.Scope = scope;
            this.Data = data;
        }

        public Event(EventKind kind, Id<Project> data)
            : this(kind, "project", data.As<Project, Element>())
        {
        }

        public Event(EventKind kind, Id<Document> data)
            : this(kind, "document", data.As<Document, Element>())
        {
        }

        public enum EventKind
        {
            Begin,
            End
        }
    }
}
