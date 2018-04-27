# 
# https://www.kaggle.com/c/word2vec-nlp-tutorial/overview
# https://www.kaggle.com/c/word2vec-nlp-tutorial#part-1-for-beginners-bag-of-words
# 
import pandas as pd

# Reading the Data

train = pd.read_csv("labeledTrainData.tsv", header=0, delimiter="\t", quoting=3)
print("train.shape: ", train.shape)
print("train.columns.values: ", train.columns.values)
print("print(train['sentiment'][0]): ", train["sentiment"][0])
print("\nprint(train['review'][0]): ", train["review"][0])

# Data Cleaning and Text Preprocessing

# Import BeautifulSoup into your workspace
from bs4 import BeautifulSoup

# Initialize the BeautifulSoup object on a single movie review     
example1 = BeautifulSoup(train["review"][0], "lxml")
print("\nBeautifulSoup(train['review'][0]): ", example1.get_text())

import re
# Use regular expressions to do a find-and-replace
letters_only = re.sub("[^a-zA-Z]",           # The pattern to search for
                      " ",                   # The pattern to replace it with
                      example1.get_text() )  # The text to search
print("\nletters_only: ", letters_only)

lower_case = letters_only.lower()        # Convert to lower case
words = lower_case.split()               # Split into words
print("found {0} words".format(len(words)))

# removed, because size of nltk data (>3.7GB)
# import nltk
# # nltk.download()  # Download text data sets, including stop words
# from nltk.corpus import stopwords # Import the stop word list
# print("stopwords.words: ", stopwords.words("english"))

stopwords =  ['i', 'me', 'my', 'myself', 'we', 'our', 'ours', 'ourselves', 'you', "you're", "you've", "you'll", "you'd", 'your', 'yours', 'yourself', 'yourselves', 'he', 'him', 'his', 'himself', 'she', "she's", 'her', 'hers', 'herself', 'it', "it's", 'its', 'itself', 'they', 'them', 'their', 'theirs', 'themselves', 'what', 'which', 'who', 'whom', 'this', 'that', "that'll", 'these', 'those', 'am', 'is', 'are', 'was', 'were', 'be', 'been', 'being', 'have', 'has', 'had', 'having', 'do', 'does', 'did', 'doing', 'a', 'an', 'the', 'and', 'but', 'if', 'or', 'because', 'as', 'until', 'while', 'of', 'at', 'by', 'for', 'with', 'about', 'against', 'between', 'into', 'through', 'during', 'before', 'after', 'above', 'below', 'to', 'from', 'up', 'down', 'in', 'out', 'on', 'off', 'over', 'under', 'again', 'further', 'then', 'once', 'here', 'there', 'when', 'where', 'why', 'how', 'all', 'any', 'both', 'each', 'few', 'more', 'most', 'other', 'some', 'such', 'no', 'nor', 'not', 'only', 'own', 'same', 'so', 'than', 'too', 'very', 's', 't', 'can', 'will', 'just', 'don', "don't", 'should', "should've", 'now', 'd', 'll', 'm', 'o', 're', 've', 'y', 'ain', 'aren', "aren't", 'couldn', "couldn't", 'didn', "didn't", 'doesn', "doesn't", 'hadn', "hadn't", 'hasn', "hasn't", 'haven', "haven't", 'isn', "isn't", 'ma', 'mightn', "mightn't", 'mustn', "mustn't", 'needn', "needn't", 'shan', "shan't", 'shouldn', "shouldn't", 'wasn', "wasn't", 'weren', "weren't", 'won', "won't", 'wouldn', "wouldn't"]
# Remove stop words from "words"
words = [w for w in words if not w in stopwords]
print("\n all words:\n", words)