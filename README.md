# TextAnalytics

Performs sentiment, key phrase, entities, emotion analysis on text
~~~sh
curl -X POST \
  https://textanalytics/api/Analyze \
  -H 'Accept: application/json' \
  -H 'Content-Type: application/json' \
  -d '"Four score and seven years ago our fathers brought forth on this continent, a new nation, conceived in Liberty, and dedicated to the proposition that all men are created equal.Now we are engaged in a great civil war, testing whether that nation, or any nation so conceived and so dedicated, can long endure."'
~~~

~~~json
{
    "keyPhrases": [
        "new nation",
        "fathers",
        "proposition",
        "continent",
        "Liberty",
        "great civil war",
        "score",
        "years",
        "men"
    ],
    "entities": [
        "Gettysburg Address"
    ],
    "sentiment": 0.78448164463043213,
    "dominance": 0.6892307692307692,
    "valence": 1.0144230769230771,
    "arousal": -0.65615384615384609
}
~~~
