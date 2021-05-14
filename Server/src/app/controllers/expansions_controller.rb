class ExpansionsController < ApplicationController
    def index
        list = Expansion.all()
        result = []
        for expansion in list do
            result.push({id: expansion.id, name: expansion.bundle_name, cost: expansion.cost})
        end
        render json: result.to_json()
    end
end
