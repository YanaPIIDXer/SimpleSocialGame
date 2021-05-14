class ExpansionsController < ApplicationController
    def index
        list = Expansion.all()
        result = []
        for expansion in list do
            result.push(expansion.bundle_name)
        end
        render json: result.to_json()
    end
end
