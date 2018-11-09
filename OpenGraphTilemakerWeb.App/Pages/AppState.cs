using System;
using System.Collections.Generic;
using System.Linq;
using OpenGraphTilemaker;

namespace OpenGraphTilemakerWeb.App.Pages
{
    public class AppState
    {
        public List<GetPocketEntry> Urls { get; set; } = new List<GetPocketEntry>();

        public List<OpenGraphMetadata> Tiles { get; set; } = new List<OpenGraphMetadata>();

        public string SortProperty { get; set; } = "Title";

        public SortOrder SortOrder { get; set; } = SortOrder.Ascending;

        public int TileCount { get; set; }

        public event Action OnSort;

        public void Sort()
        {
            switch (SortProperty)
            {
                case "Title":
                    Tiles = SortOrder == SortOrder.Ascending
                        ? Tiles.OrderBy(f => f?.Title == null).ThenBy(f => f?.Title).ToList()
                        : Tiles.OrderByDescending(f => f?.Title != null).ThenByDescending(f => f?.Title).ToList();
                    break;
                case "PubDate":
                    Tiles = SortOrder == SortOrder.Ascending
                        ? Tiles.OrderBy(f => f?.ArticlePublishedTime != null).ThenBy(f => f?.ArticlePublishedTime).ToList()
                        : Tiles.OrderByDescending(f => f?.ArticlePublishedTime != null)
                            .ThenByDescending(f => f?.ArticlePublishedTime).ToList();
                    break;
            }

            TileCount = Tiles.Count;

            StateChanged();
        }

        private void StateChanged() => OnSort?.Invoke();
    }
}