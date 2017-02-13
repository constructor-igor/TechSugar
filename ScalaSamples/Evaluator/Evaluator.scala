package main

import org.apache.spark.SparkContext
import org.apache.spark.SparkConf
import org.apache.spark.SparkContext._

object WordCounter {
	def main(args: Array[String]) {
		val conf = new SparkConf().setAppName("Evaluator")
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
    }
}    