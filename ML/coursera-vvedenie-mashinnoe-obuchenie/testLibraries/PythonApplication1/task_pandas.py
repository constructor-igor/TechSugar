import pandas

def find_element_in_list(element, list_element):
    try:
        index_element = list_element.index(element)
        return index_element
    except ValueError:
        return None

def clearName(rawName):
    name = rawName
    name = name.replace("(", "")
    name = name.replace("", "")
    return name

def calculate(fullName):
    if "(" in fullName:
        bracketsName = fullName[fullName.index("(") + 1:fullName.rindex(")")]
        return clearName(bracketsName.split()[0])
    nameItems = fullName.split()
    if "Miss." in nameItems:
        missIndex = find_element_in_list("Miss.", nameItems)
        return clearName(nameItems[missIndex+1])
    if "Mrs." in nameItems:
        mrsIndex = find_element_in_list("Mrs.", nameItems)
        return clearName(nameItems[mrsIndex+1])
    return ""

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
print ""
print "SibSp data:"
print sibSpColumn.value_counts()
print ""
print "Parch data:"
print parchColumn.value_counts()

mean1 = sibSpColumn.mean()
mean2 = parchColumn.mean()
std1 = sibSpColumn.std()
std2 = parchColumn.std()
corr = ((sibSpColumn*parchColumn).mean()-mean1*mean2)/(std1*std2)
print ("corr = %s" % (corr))

femaleSet = data.ix[(data['Sex']=="female")]
femaleNamesColumn = femaleSet['Name']
print ("Female names (%s):" % (femaleNamesColumn.count()))
print femaleNamesColumn.value_counts()

firstFemaleNamesColumn = femaleNamesColumn.map(calculate)
print ""
print ("First Female names (%s):" % (firstFemaleNamesColumn.count()))
print firstFemaleNamesColumn.value_counts()