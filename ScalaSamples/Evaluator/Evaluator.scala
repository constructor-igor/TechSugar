package main

import org.apache.spark.SparkContext
import org.apache.spark.SparkConf
import org.apache.spark.SparkContext._

object Evaluator {
	def main(args: Array[String]) {
		val conf = new SparkConf().setAppName("Language Evaluator")
		val sc = new SparkContext(conf)

        val wikiDocuments = sc.hadoopRDD(jobConf,
            classOf[org.apache.hadoop.streaming.StreamInputFormat],
            classOf[Text], classOf[Text])

        val deHadoopedWikis = wikiDocuments.map(hadoopXML=>hadoopXML._1.toString)

        import scala.xml.XML
        val rawWikiPages = deHadoopedWikis.map(wikiString=>{
            val wikiXML = XML.loadString(wikiString)
            val wikiPageText = (wikiXML \ "revision" \ "text").text
            WikiCleaner.parse(wikiPageText)

        val tokenizedWikiData = rawWikiPages.flatMap(wikiText=>wikiText.split("\\W+"))
        val pertinentWikiData = tokenizedWikiData
                                .map(wikiToken => wikiToken.replaceAll("[.|,|'|\"|?|)|(]", "").trim)
                                .filter(wikiToken=>wikiToken.length > 2)

        val wikiDataSortedByLength = pertinentWikiData.distinct    
                .sortBy(wikiToken=>wikiToken.length, accending = false)
                .sample(withReplacement = false, fraction = 0.1)
                .keyBy(wikiToken=>wikiToken.length)

        wikiDataSortedByLength.collect.foreach(println)                
    }
}    