using System;
using System.Linq;
using Lime.Protocol;
using Lime.Messaging.Contents;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace desafio
{
    public class CarrasoulLoading
    {

        public readonly Reader reader;

        public CarrasoulLoading()
        {
            this.reader = new Reader();
        }

        public DocumentCollection Culturas()
        {
            var culturaTake = reader.ReadFile();

            var documents = new DocumentSelect[culturaTake.Count()];
            var position = 0;

            foreach (var item in culturaTake)
            {

                documents[position] =
                    new DocumentSelect
                    {
                        Header = new DocumentContainer
                        {
                            Value = new MediaLink
                            {
                                Title = item.Title,
                                Text = item.Description,
                                Type = "image/jpeg",
                                Uri = new Uri(item.PreviewUri),
                            }
                        },
                        Options = new DocumentSelectOption[]
                        {
                            new DocumentSelectOption
                            {
                                Label = new DocumentContainer
                                {
                                    Value = new WebLink
                                    {
                                        Title = "Site Take Blip",
                                        Uri = new Uri("https://www.take.net/")
                                    }
                                }
                            }
                        }
                    };

                position++;
            }

            var document = new DocumentCollection
            {
                ItemType = "application/vnd.lime.document-select+json",
                Items = documents,
            };

            return document;
        }
    }
}
