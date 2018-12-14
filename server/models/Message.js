const mongoose = require('mongoose')

const MessageSchema = new mongoose.Schema({
  text: {
    type: String,
    required: true
  },
  location: {
    type: {
      type: String,
      enum: ['Point'],
      required: true
    },
    coordinates: {
      type: [Number],
      required: true
    }
  },
  altitude: {
    type: Number,
    required: true
  },
  points: {
    type: Number,
    required: true,
    default: 0
  }
})

MessageSchema.index({location: '2dsphere'})

const Message = mongoose.model('Message', MessageSchema)

module.exports = Message