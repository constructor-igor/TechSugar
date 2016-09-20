import numpy as np
import pandas as pd
import matplotlib.pyplot as plt
plt.style.use('ggplot')

trainingFilePath = 'trainingData.csv'
data = pd.read_csv(trainingFilePath)