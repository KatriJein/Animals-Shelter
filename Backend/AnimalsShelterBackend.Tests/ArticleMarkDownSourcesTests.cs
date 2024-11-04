using Core.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AnimalsShelterBackend.Tests
{
	[TestFixture]
	public class ArticleMarkDownSourcesTests
	{
		[TestCase("")]
		[TestCase("<h1>there is some simple text</h1>")]
		[TestCase("<h1 src=\"unexpected source\">there is some more text and incorrect tag</h1><img alt=\"hello\">")]
		[TestCase("<video controls><audio controls><img>")]
		public void MatchOnSimpleMarkDownWithNoCorrectSrcReturnsNoSources_Tests(string markdown)
		{
			var matches = Const.GetUrlsFromArticleRegex.Matches(markdown);
			var urls = matches.Select(m => m.Groups[2].Value).ToArray();
			Assert.That(urls, Is.Empty);
		}

		[TestCase("onlytext", new string[] { })]
		[TestCase("simple_page", new string[] { "image1.jpg" })]
		[TestCase("Multimedia_page", new string[] { "image1.jpg", "video1.mp4" })]
		[TestCase("Gallery_page", new string[] { "image1.jpg", "image2.jpg", "audio1.mp3" })]
		[TestCase("media_showcase", new string[] { "image1.jpg", "image2.jpg", "audio1.mp3", "image3.jpg", "audio2.mp3", "video1.mp4" })]
		[TestCase("media_full", new string[] { "image1.jpg", "image2.jpg", "audio1.mp3", "image3.jpg", "audio2.mp3", "video1.mp4", "image4.jpg", "video2.mp4", "audio3.mp3" })]
		[TestCase("article1", new string[] { "ocean_life.jpg", "ocean_waves.mp3" })]
		[TestCase("article2", new string[] { "telescope_view.jpg", "black_hole_theory.mp4", "space_sounds.mp3" })]
		[TestCase("article3", new string[] { "solar_panels.jpg", "wind_farm.mp4", "wind_turbine.jpg", "hydropower_sound.mp3" })]

		public void MatchOnActualMarkDownsReturnsSources_Tests(string fileName, string[] expectedUrls)
		{
			var filePath = Path.Combine("..", "..", "..", "MarkDowns", $"{fileName}.txt");
			var content = File.ReadAllText(filePath);
			var matches = Const.GetUrlsFromArticleRegex.Matches(content);
			var urls = matches.Select(m => m.Groups[2].Value).ToArray();
			Assert.That(urls, Is.EquivalentTo(expectedUrls));
		}
	}
}
