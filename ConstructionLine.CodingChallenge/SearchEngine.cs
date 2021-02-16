using System.Collections.Generic;
using System.Linq;

namespace ConstructionLine.CodingChallenge
{
  
    public class SearchEngine 
    {
        private  List<Shirt> _shirts;

        private readonly SearchResults _searchResults;
        
        private readonly IShirtBuilder _shirtBuilder;

        public SearchEngine(IShirtBuilder shirtBuilder)
        {
            _shirtBuilder = shirtBuilder;
            _searchResults = new SearchResults();
            _shirts = _shirtBuilder.CreateShirts();

            // TODO: data preparation and initialisation of additional data structures to improve performance goes here.

        }

        public SearchResults Search(SearchOptions options)
        {
            // TODO: search logic goes here.
            
            foreach (var shirt in _shirts)
            {
                foreach (var color in options.Colors)
                {
                    foreach (var size in options.Sizes.Where(size => shirt.Color.Name == color.Name && shirt.Size.Name == size.Name))
                    {
                        _searchResults.Shirts.Add(shirt);
                        var colorCount = _searchResults.ColorCounts.FirstOrDefault(c => c.Color.Name == shirt.Color.Name);
                        var sizeCount = _searchResults.SizeCounts.FirstOrDefault(s => s.Size.Name == shirt.Size.Name);

                        if (sizeCount == null)
                        {
                            _searchResults.SizeCounts.Add(new SizeCount() { Size = size, Count = 1 });
                        }
                        else
                        {
                            sizeCount.Count += 1;
                        }

                        if (colorCount == null)
                        {
                            _searchResults.ColorCounts.Add(new ColorCount() { Color = shirt.Color, Count = 1 });
                        }
                        else
                        {
                            colorCount.Count += 1;
                        }
                    }
                }
            }

            return _searchResults;
        }
    }
}