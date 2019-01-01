# 
# project: https://newspaper.readthedocs.io/en/latest/
# demo: http://newspaper-demo.herokuapp.com/
# 
import os
from newspaper import Article, fulltext
from nltk.corpus import stopwords
import nltk
import string

Negative = 0
Positive = 1
Debatable_Postivive = 2

def ensure_dir(directory):
    if not os.path.exists(directory):
        os.makedirs(directory)

def url_to_html_file_name(url):
    html_file_name = url.replace(':', '_').replace('/', '_') + ".html"
    return html_file_name

def write_text_to_file(file_path, text_content):
    with open(file_path, "w", encoding='utf-8') as text_file:
        print(text_content, file=text_file)

def read_text_from_file(file_path):
    with open(file_path, encoding='utf-8') as f: 
        data_content = f.read()
    return data_content

if __name__ == "__main__":
    training_set = []
    training_set.append(('https://www.huffingtonpost.com/entry/frenchman-trying-to-cross-atlantic-in-barrel-capsule_us_5c2641d0e4b08aaf7a8ffe93', Positive))
    training_set.append(('https://www.huffingtonpost.com/entry/antarctica-colin-o-brady-solo-crossing_us_5c24510fe4b05c88b6fd7546', Positive))
    training_set.append(('https://www.huffingtonpost.com/entry/indonesia-tsunami-death-toll-429_us_5c227dd7e4b08aaf7a8c2cb6', Negative))
    training_set.append(('https://www.huffingtonpost.com/entry/mexico-helicopter-crash-claims-puebla-governor-ex-governor_us_5c21a568e4b05c88b6fb84f4', Negative))
    training_set.append(('https://www.huffingtonpost.com/entry/egypt-ancient-tomb-discovered_us_5c15aa05e4b05d7e5d8298ef', Positive))
    training_set.append(('https://www.huffingtonpost.com/entry/sarah-papenheim-man-arrested_us_5c0dd2f1e4b0ab8cf694a501', Negative))
    training_set.append(('https://www.huffingtonpost.com/entry/australia-recognizes-west-jerusalem-israels-capital_us_5c14cfbce4b05d7e5d825018', Debatable_Postivive))
    training_set.append(('https://www.huffingtonpost.com/entry/luxembourg-public-transportation_us_5c08d6ace4b069028dc685ac', Positive))

    data_folder = r'.\data'
    ensure_dir(data_folder)

    # https://stackoverflow.com/questions/5486337/how-to-remove-stop-words-using-nltk-or-python
    # https://stackoverflow.com/questions/265960/best-way-to-strip-punctuation-from-a-string-in-python
    nltk_words = list(stopwords.words('english')) #About 150 stopwords
    exclude = list(string.punctuation)
    stop_words = nltk_words
    stop_words.extend(exclude)
    stop_words.extend(['—', '’'])
    
    print(f"loaded {len(stop_words)} stop words and punctuation")

    for url, expected in training_set:
        print(f"\n{url}")
        
        target_file_name = url_to_html_file_name(url)
        target_hthml_file = os.path.join(data_folder, target_file_name)
        text = None
        if not os.path.exists(target_hthml_file):
            article = Article(url)
            article.download()
            print(f"url: {article.url}")
            print(f"source_url: {article.source_url}")
            print(f"message: {article.download_exception_msg}")
            if article.download_exception_msg is None:
                html = article.html
                write_text_to_file(target_hthml_file, html)

                article.parse()
                print(f"authors: {article.authors}")
                print(f"publish_date: {article.publish_date}")
                print(f"text: {article.text}")
                article.nlp()
                print(f"key words: {article.keywords}")
                print(f"summary: {article.summary}")
                text = article.text
        else:
            article = Article(url)
            html_text = read_text_from_file(target_hthml_file)            
            text = fulltext(html_text,'en')

        if text is not None:
            # print(f"text: {text}")
            # removing stop words and punctuation
            all_article_words = nltk.word_tokenize(text)
            article_tokens = [w for w in all_article_words if not w in stop_words]
            print(f"loaded: {len(article_tokens)} actual from {len(all_article_words)} total words")
            # TODO stemming