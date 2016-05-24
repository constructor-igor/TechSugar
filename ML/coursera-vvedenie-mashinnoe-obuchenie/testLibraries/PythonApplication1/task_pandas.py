import pandas
data = pandas.read_csv('titanic.csv', index_col='PassengerId')
count = data['Pclass'].value_counts()
print count

sexColumn = data['Sex']
sexCount = sexColumn.value_counts()
print sexCount

survivedColumn = data['Survived']
survivedData = survivedColumn.value_counts()
survivedCount = survivedData[1]
totalCount = survivedColumn.count()
survivedProcent = float(survivedCount)/float(totalCount)*100
print(" %s from %s " % (survivedCount, totalCount))
print survivedProcent

pclassColumn = data['Pclass']
pclassTotal = pclassColumn.count()
pclassData = pclassColumn.value_counts()
class1Count = pclassData[1]
class1Procent =  float(class1Count)/float(pclassTotal)*100
print(" %s from %s " % (class1Count, pclassTotal))
print class1Procent

ageColumn = data['Age']
ageData = ageColumn.value_counts()
ageMean = ageColumn.mean()
ageMedian = ageColumn.median()
print ageData
print ("ageMean = %s, ageMedian = %s" % (ageMean, ageMedian))

sibSpColumn = data['SibSp']
parchColumn = data['Parch']
print sibSpColumn.value_counts()
print parchColumn.value_counts()