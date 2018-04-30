# 
# https://www.kaggle.com/c/word2vec-nlp-tutorial/overview
# https://www.kaggle.com/c/word2vec-nlp-tutorial#part-1-for-beginners-bag-of-words
# 
# kaggle competitions submit -c word2vec-nlp-tutorial -f submission.csv -m "Message"
# 
import pandas as pd
from bs4 import BeautifulSoup       # Import BeautifulSoup into your workspace
import re
import numpy as np


def experience():
    # Reading the Data

    train = pd.read_csv("labeledTrainData.tsv", header=0, delimiter="\t", quoting=3)
    print("train.shape: ", train.shape)
    print("train.columns.values: ", train.columns.values)
    print("print(train['sentiment'][0]): ", train["sentiment"][0])
    print("\nprint(train['review'][0]): ", train["review"][0])

    # Data Cleaning and Text Preprocessing

    # Initialize the BeautifulSoup object on a single movie review     
    example1 = BeautifulSoup(train["review"][0], "lxml")
    print("\nBeautifulSoup(train['review'][0]): ", example1.get_text())

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

def review_to_words(raw_review):
    stopwords =  ['i', 'me', 'my', 'myself', 'we', 'our', 'ours', 'ourselves', 'you', "you're", "you've", "you'll", "you'd", 'your', 'yours', 'yourself', 'yourselves', 'he', 'him', 'his', 'himself', 'she', "she's", 'her', 'hers', 'herself', 'it', "it's", 'its', 'itself', 'they', 'them', 'their', 'theirs', 'themselves', 'what', 'which', 'who', 'whom', 'this', 'that', "that'll", 'these', 'those', 'am', 'is', 'are', 'was', 'were', 'be', 'been', 'being', 'have', 'has', 'had', 'having', 'do', 'does', 'did', 'doing', 'a', 'an', 'the', 'and', 'but', 'if', 'or', 'because', 'as', 'until', 'while', 'of', 'at', 'by', 'for', 'with', 'about', 'against', 'between', 'into', 'through', 'during', 'before', 'after', 'above', 'below', 'to', 'from', 'up', 'down', 'in', 'out', 'on', 'off', 'over', 'under', 'again', 'further', 'then', 'once', 'here', 'there', 'when', 'where', 'why', 'how', 'all', 'any', 'both', 'each', 'few', 'more', 'most', 'other', 'some', 'such', 'no', 'nor', 'not', 'only', 'own', 'same', 'so', 'than', 'too', 'very', 's', 't', 'can', 'will', 'just', 'don', "don't", 'should', "should've", 'now', 'd', 'll', 'm', 'o', 're', 've', 'y', 'ain', 'aren', "aren't", 'couldn', "couldn't", 'didn', "didn't", 'doesn', "doesn't", 'hadn', "hadn't", 'hasn', "hasn't", 'haven', "haven't", 'isn', "isn't", 'ma', 'mightn', "mightn't", 'mustn', "mustn't", 'needn', "needn't", 'shan', "shan't", 'shouldn', "shouldn't", 'wasn', "wasn't", 'weren', "weren't", 'won', "won't", 'wouldn', "wouldn't"]
    # Function to convert a raw review to a string of words
    # The input is a single string (a raw movie review), and 
    # the output is a single string (a preprocessed movie review)
    #
    # 1. Remove HTML
    review_text = BeautifulSoup(raw_review).get_text() 
    #
    # 2. Remove non-letters        
    letters_only = re.sub("[^a-zA-Z]", " ", review_text) 
    #
    # 3. Convert to lower case, split into individual words
    words = letters_only.lower().split()                             
    #
    # 4. In Python, searching a set is much faster than searching
    #   a list, so convert the stop words to a set
    stops = set(stopwords)                  
    # 
    # 5. Remove stop words
    meaningful_words = [w for w in words if not w in stops]   
    #
    # 6. Join the words back into one string separated by space, 
    # and return the result.
    return( " ".join( meaningful_words )) 

if __name__ == "__main__":
    print("[main] started")
    # experience()
    train = pd.read_csv("labeledTrainData.tsv", header=0, delimiter="\t", quoting=3)
    clean_review = review_to_words(train["review"][0] )

    print("\nclean_review: ", clean_review)

    # Get the number of reviews based on the dataframe column size
    num_reviews = train["review"].size
    print("\nnum_reviews: ", num_reviews)
    # Initialize an empty list to hold the clean reviews
    clean_train_reviews = []
    for index in range(num_reviews):
        # If the index is evenly divisible by 1000, print a message
        if((index+1)%1000 == 0):
            print("Review %d of %d\n" % (index+1, num_reviews))
        clean_review = review_to_words(train["review"][index])
        clean_train_reviews.append(clean_review)

    print("Creating the bag of words...\n")
    from sklearn.feature_extraction.text import CountVectorizer

    # Creating Features from a Bag of Words (Using scikit-learn)

    # Initialize the "CountVectorizer" object, which is scikit-learn's
    # bag of words tool.
    vectorizer = CountVectorizer(analyzer = "word", tokenizer = None, preprocessor = None, stop_words = None, max_features = 5000) 
    # fit_transform() does two functions: First, it fits the model
    # and learns the vocabulary; second, it transforms our training data
    # into feature vectors. The input to fit_transform should be a list of 
    # strings.
    train_data_features = vectorizer.fit_transform(clean_train_reviews)
    print("train_data_features.shape: ", train_data_features.shape)

    # Take a look at the words in the vocabulary
    vocab = vectorizer.get_feature_names()
    # print("vocab: ", vocab)

    # Sum up the counts of each vocabulary word
    dist = np.sum(train_data_features, axis=0)
    # For each, print the vocabulary word and the number of times it 
    # appears in the training set
    # for tag, count in zip(vocab, dist):
    #     print("\n[{1}]: {0}".format(count, tag))

    # Random Forest
    from sklearn.ensemble import RandomForestClassifier
    print("Training the random forest...")

    # Initialize a Random Forest classifier with 100 trees
    forest = RandomForestClassifier(n_estimators = 100)

    # Fit the forest to the training set, using the bag of words as 
    # features and the sentiment labels as the response variable
    #
    # This may take a few minutes to run
    forest = forest.fit(train_data_features, train["sentiment"])

    # Creating a Submission
    # Read the test data
    test = pd.read_csv("testData.tsv", header=0, delimiter="\t", quoting=3)
    # Verify that there are 25,000 rows and 2 columns
    print(test.shape)
    # Create an empty list and append the clean reviews one by one
    num_reviews = len(test["review"])
    clean_test_reviews = []
    print("Cleaning and parsing the test set movie reviews...\n")
    for i in range(num_reviews):
        if( (i+1) % 1000 == 0 ):
            print("Review %d of %d\n" % (i+1, num_reviews))
        clean_review = review_to_words( test["review"][i] )
        clean_test_reviews.append( clean_review )

    # Get a bag of words for the test set, and convert to a numpy array
    test_data_features = vectorizer.transform(clean_test_reviews)
    test_data_features = test_data_features.toarray()

    # Use the random forest to make sentiment label predictions
    result = forest.predict(test_data_features)

    # Copy the results to a pandas dataframe with an "id" column and
    # a "sentiment" column
    output = pd.DataFrame(data={"id":test["id"], "sentiment":result})

    # Use pandas to write the comma-separated output file
    output.to_csv("Bag_of_Words_model.csv", index=False, quoting=3)

    print("[main] finished")