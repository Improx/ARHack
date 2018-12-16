const express = require('express')
const Message = require('./models/Message')
const mongoose = require('mongoose')
const connect = require('connect')
const bodyParser = require('body-parser')
const database = 'mongodb://localhost:27017'

const app = express()
const port = 3000

mongoose.connect(database)

app.use(bodyParser.urlencoded({extended: false, limit: '100mb'}))

app.get('/', (req, res) => res.send('Hello World!'))

app.get('/messages/:lat/:long/:distance', (req, res) => {
  const {lat, long, distance} = req.params
  Message.find({
    location: {
      $near: {
        $geometry: {
          type: 'Point' ,
          coordinates: [ +lat , +long ]
       },
       $maxDistance: +distance,
       $minDistance: 0
      }
    }
  }, (err, result) => {
    if(err || !result) {
      return res.status(500).send(err)
    }

    const messages = {result}

    return res.status(200).send(messages);
    
  })
})

app.get('/messages', (req, res) => {
  const {lat, long} = req.params
  Message.find({}, (err, result) => {
    if(err || !result) {
      return res.status(500).send(err)
    }

    const messages = {result}

    return res.status(200).send(messages);
    
  })
})

app.post('/messages', (req, res) => {
  console.log('UPLOADING MESSAGE')
  const {text, latitude, longitude, altitude, animation, modelId} = req.body

  const animationParsed = JSON.parse(animation).frames

  console.log(animationParsed.length ? `Total frames in animation: ${animationParsed.length}` : 'No frames')
  console.log(`Model id: ${modelId}`)

  if(!text || !latitude || !longitude || !altitude) {
    return res.status(500).send('Missing required parameters!')
  }

  const message = new Message({
    text,
    location: {
      type: 'Point',
      coordinates: [+latitude, +longitude]
    },
    altitude: +altitude,
    animation: animationParsed,
    modelId: modelId
  })

  message.save()
   .then(doc => {
     console.log('New message saved!')
     return res.status(200).send(doc)
   })
   .catch(err => {
     console.log('error:')
     console.error(err)
     return res.send(500).send(err)
   })
})

app.put('/messages/upvote/:id', (req, res) => {
  const {id} = req.params
  console.log(`Upvoting ${id}`)
  Message.findById(id, (err, message) => {
    if (err) return handleError(err);
    message.points += 1;
    message.save(function (err, updatedMessage) {
      if (err) return handleError(err);
      res.send(updatedMessage);
    });
  });
})

app.put('/messages/downvote/:id', (req, res) => {
  const {id} = req.params
  console.log(`Downvoting ${id}`)
  Message.findById(id, (err, message) => {
    if (err) return handleError(err);
    message.points -= 1;
    message.save(function (err, updatedMessage) {
      if (err) return handleError(err);
      res.send(updatedMessage);
    });
  });
})

app.listen(port, () => console.log(`Example app listening on port ${port}!`))