class ExpansionsController < ApplicationController
    def index
        list = Expansion.all()
        result = []
        for expansion in list do
            result.push({id: expansion.id, name: expansion.bundle_name, cost: expansion.cost})
        end
        render json: result.to_json()
    end

    def draw
        user = User.find_by(id: session[:id])
        if user == nil then
            render json: {message: "error."}, status: 304
            return
        end

        response = {result: false, card_name: "", last_stone: user.stone}
        id = params["id"]
        expansion = Expansion.find_by(id: id)
        if expansion == nil then
            render json: {message: "error."}, status: 304
            return
        end

        if user.stone >= expansion.cost then
            user.stone -= expansion.cost
            response[:last_stone] = user.stone
            user.save()

            cards = expansion.card
            rnd = Random.new()
            index = rnd.rand(cards.length)
            card = cards[index]
            response[:card_name] = card.asset_name

            response[:result] = true
        end

        render json: response
    end
end
