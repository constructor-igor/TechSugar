

import findspark
findspark.init()

from pyspark.ml.regression import LinearRegression, LinearRegressionModel
from pyspark.ml.linalg import Vectors, VectorUDT
from pyspark.sql import SparkSession
from pyspark.sql.types import *
from pyspark.sql import SQLContext, Row
from pyspark.sql.types import FloatType
from pyspark.sql import functions


def convert_to_flat_by_pandas(df):
    pandas_data_frame = df.toPandas()
    all_keys = pandas_data_frame['key'].unique()

    flat_values = []
    for key in all_keys:
        key_rows = pandas_data_frame.loc[pandas_data_frame['key'] == key]
        key_rows = key_rows.sort_values(by=['subkey'])
        
        parameter_values = key_rows['parameter']
        parameter_value = parameter_values.real[0]        

        # key_reference_value = []
        # for reference_values in key_rows['reference']:
        #     key_reference_value.append(reference_values)
        key_reference_value = [reference_values for reference_values in key_rows['reference']]

        flat_values.append((parameter_value, key_reference_value))

    loaded_data = [(label, Vectors.dense(features)) for (label, features) in flat_values]
    spark_df = spark.createDataFrame(loaded_data, ["label", "features"])

    return spark_df

if __name__ == "__main__":
    print("main started")

    spark = SparkSession\
        .builder\
        .appName("df-flat-pyspark")\
        .getOrCreate()

    data = [
        {'key': 'key1', 'subkey': 'subkey1', 'parameter': '45', 'reference': '10'},
        {'key': 'key1', 'subkey': 'subkey2', 'parameter': '45', 'reference': '20'},
        {'key': 'key2', 'subkey': 'subkey2', 'parameter': '70', 'reference': '40'},
        {'key': 'key2', 'subkey': 'subkey1', 'parameter': '70', 'reference': '30'}
    ]
    
    original_df = spark.createDataFrame(data)
    print('original data:')
    original_df.show()

    flat_df = convert_to_flat_by_pandas(original_df)
    print('result data (by pandas):')
    flat_df.show()

    print("main completed")