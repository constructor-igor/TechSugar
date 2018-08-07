import numpy as np

import findspark
findspark.init()

from pyspark.sql import SparkSession

if __name__ == "__main__":
    print('[main] started')
    spark = SparkSession\
        .builder\
        .appName("pyspark-samples")\
        .getOrCreate()
    print("spark version:", spark.version)
    sc = spark.sparkContext

    rdd = sc.parallelize([1, 2, 3, 4, 5])
    print(rdd.collect())

    print('[main] finished')


# from pyspark.ml.regression import LinearRegression, LinearRegressionModel
# from pyspark.ml.linalg import Vectors, VectorUDT
# from pyspark.sql import SparkSession
# from pyspark.sql.types import *
# from pyspark.sql import SQLContext, Row
# from pyspark.sql.types import FloatType
# # from pyspark.sql import 
# # from pyspark.sql.functions import first, col
# from pyspark.sql.functions import *
# from pyspark.ml.feature import VectorAssembler
